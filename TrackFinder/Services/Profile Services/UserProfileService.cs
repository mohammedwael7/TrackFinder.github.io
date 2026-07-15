using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Context;
using TrackFinder.Models.CommunityModels;
using TrackFinder.Models.UserModels;
using TrackFinder.ViewModels.Auth_ViewModels;
using TrackFinder.ViewModels.Profile_ViewModels;

namespace TrackFinder.Services.UserProfileServices
{
    public class UserProfileService(AppDbContext db, UserManager<User> userManager) : IUserProfileService
    {
        public async Task<DashboardVM> GetDashboardDataAsync(Guid userId, string role)
        {
            var model = new DashboardVM
            {
                Role = role
            };

            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null) return model;
            model.User = user;

            if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                var student = await db.Students
                    .Include(s => s.Enrollments!)
                        .ThenInclude(e => e.Course)
                            .ThenInclude(c => c.Lessons)
                                .ThenInclude(l => l.Exams)
                    .Include(s => s.Enrollments!)
                        .ThenInclude(e => e.Course)
                            .ThenInclude(c => c.Materials)
                    .Include(s => s.AssessmentResults!)
                        .ThenInclude(ar => ar.RecommendedTracks)
                            .ThenInclude(art => art.Track)
                    .FirstOrDefaultAsync(s => s.UserId == userId);

                model.EnrolledCourses = student?.Enrollments?.ToList() ?? [];

                model.RecommendedTracks = student?.AssessmentResults?
                    .OrderByDescending(ar => ar.CreatedAt)
                    .SelectMany(ar => ar.RecommendedTracks)
                    .Select(art => art.Track)
                    .Distinct()
                    .ToList() ?? [];
            }

            if (role.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
            {
                var instructor = await db.Instructors
                    .Include(i => i.CreatedCourses!)
                        .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Exams)
                    .Include(i => i.CreatedCourses!)
                        .ThenInclude(c => c.Enrollments)
                    .FirstOrDefaultAsync(i => i.UserId == userId);

                model.CreatedCourses = instructor?.CreatedCourses?.ToList() ?? [];
                model.TotalLessonsCount = instructor?.CreatedCourses?.Sum(c => c.Lessons.Count) ?? 0;
                model.TotalStudentsCount = instructor?.CreatedCourses?.Sum(c => c.Enrollments?.Count ?? 0) ?? 0;
            }

            model.RecentPosts = await db.Posts
                .Include(p => p.Author)
                .Include(p => p.Community)
                .Where(p => p.Status == PostStatus.Approved)
                .OrderByDescending(p => p.CreatedAt)
                .Take(3)
                .ToListAsync();

            return model;
        }

        public async Task<EditProfileVM> GetProfileForEditAsync(Guid userId, string role)
        {
            var user = await db.Users
                .Include(u => u.Student)
                .Include(u => u.Instructor)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new InvalidOperationException("User not found.");

            var dto = new EditProfileVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Gender = user.Gender.ToString(),
                Bio = user.Bio,
                ProfilePictureUrl = user.ProfilePictureUrl
            };

            if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                dto.EducationState = user.Student?.EducationState;
                dto.SchoolOrUnversityName = user.Student?.SchoolOrUnversityName;
                dto.Major = user.Student?.Major;
                dto.Minor = user.Student?.Minor;
                dto.DegreeProgram = user.Student?.DegreeProgram;
                dto.AcademicYear = user.Student?.AcademicYear ?? 0;
                dto.GPA = user.Student?.GPA ?? 0;
            }
            else if (role.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
            {
                dto.Title = user.Instructor?.Title;
                dto.GithubLink = user.Instructor?.GithubLink;
                dto.LinkedInLink = user.Instructor?.LinkedInLink;
            }

            return dto;
        }

        public async Task<AuthResultVM> UpdateProfileAsync(Guid userId, string role, EditProfileVM dto, string webRootPath)
        {
            var user = await db.Users
                .Include(u => u.Student)
                .Include(u => u.Instructor)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return AuthResultVM.Fail("User not found.");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Birthdate = dto.Birthdate;

            if (!string.IsNullOrWhiteSpace(dto.Gender) && Enum.TryParse<Gender>(dto.Gender, true, out var parsedGender))
            {
                user.Gender = parsedGender;
            }

            user.Bio = dto.Bio;

            if (dto.ProfilePicture is { Length: > 0 })
            {
                string[] allowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".webp"];
                var extension = Path.GetExtension(dto.ProfilePicture.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    return AuthResultVM.Fail("Only JPG, JPEG, PNG, GIF, or WEBP images are allowed.");
                }

                var uploadsDir = Path.Combine(webRootPath, "uploads", "profiles");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                {
                    var oldPath = Path.Combine(webRootPath, user.ProfilePictureUrl.TrimStart('/'));
                    if (File.Exists(oldPath))
                    {
                        try { File.Delete(oldPath); } catch { }
                    }
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var fullPath = Path.Combine(uploadsDir, fileName);

                await using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ProfilePicture.CopyToAsync(stream);
                }

                user.ProfilePictureUrl = $"/uploads/profiles/{fileName}";
            }

            if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                if (user.Student == null)
                {
                    user.Student = new Student { UserId = userId };
                    db.Students.Add(user.Student);
                }

                user.Student.EducationState = dto.EducationState;
                user.Student.SchoolOrUnversityName = dto.SchoolOrUnversityName;
                user.Student.Major = dto.Major;
                user.Student.Minor = dto.Minor;
                user.Student.DegreeProgram = dto.DegreeProgram;
                user.Student.AcademicYear = dto.AcademicYear;
                user.Student.GPA = dto.GPA;
            }
            else if (role.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
            {
                if (user.Instructor == null)
                {
                    user.Instructor = new Instructor { UserId = userId };
                    db.Instructors.Add(user.Instructor);
                }

                user.Instructor.Title = dto.Title;
                user.Instructor.GithubLink = dto.GithubLink;
                user.Instructor.LinkedInLink = dto.LinkedInLink;
            }

            var identityResult = await userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {
                var error = string.Join(" ", identityResult.Errors.Select(e => e.Description));
                return AuthResultVM.Fail(error);
            }

            await db.SaveChangesAsync();

            return AuthResultVM.Ok("/Main", "Profile updated successfully!");
        }
    }
}