using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrackFinder.DTOs;
using TrackFinder.DTOs.AuthenticationDTOs;
using TrackFinder.Models.AssessmentModels;
using TrackFinder.Models.CommunityModels;
using TrackFinder.Models.TeachingModels;
using TrackFinder.Models.TrackFinderDbContext;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Services.UserProfileServices
{
    public class UserProfileService : IUserProfileService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<User> _userManager;

        public UserProfileService(AppDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync(Guid userId, string role)
        {
            var model = new DashboardViewModel
            {
                Role = role
            };

            // Fetch User
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return model;
            model.User = user;

            // ── 1. Fetch Student-Specific Stats ──────────────────────────────
            if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                var student = await _db.Students
                    .Include(s => s.Enrollments!)
                        .ThenInclude(e => e.Course)
                            .ThenInclude(c => c.Lessons)
                                .ThenInclude(l => l.Exams)
                    .Include(s => s.Enrollments!)
                        .ThenInclude(e => e.Course)
                            .ThenInclude(c => c.Materials)
                    .Include(s => s.AssessmentResults!)
                        .ThenInclude(ar => ar.RecommendedTracks)
                    .FirstOrDefaultAsync(s => s.UserId == userId);

                model.EnrolledCourses = student?.Enrollments?.ToList() ?? new List<Enrollment>();

                // Get all recommended tracks across all assessment results
                model.RecommendedTracks = student?.AssessmentResults?
                    .OrderByDescending(ar => ar.CreatedAt)
                    .SelectMany(ar => ar.RecommendedTracks)
                    .Distinct()
                    .ToList() ?? new List<Track>();
            }

            // ── 2. Fetch Instructor-Specific Stats ───────────────────────────
            if (role.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
            {
                var instructor = await _db.Instructors
                    .Include(i => i.CreatedCourses!)
                        .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Exams)
                    .Include(i => i.CreatedCourses!)
                        .ThenInclude(c => c.Enrollments)
                    .FirstOrDefaultAsync(i => i.UserId == userId);

                model.CreatedCourses = instructor?.CreatedCourses?.ToList() ?? new List<Course>();
                model.TotalLessonsCount = instructor?.CreatedCourses?.Sum(c => c.Lessons.Count) ?? 0;
                model.TotalStudentsCount = instructor?.CreatedCourses?.Sum(c => c.Enrollments?.Count ?? 0) ?? 0;
            }

            // ── 3. Fetch Recent Community Posts (last 3 approved) ────────────
            model.RecentPosts = await _db.Posts
                .Include(p => p.Author)
                .Include(p => p.Community)
                .Where(p => p.Status == PostStatus.Approved)
                .OrderByDescending(p => p.CreatedAt)
                .Take(3)
                .ToListAsync();

            return model;
        }

        public async Task<EditProfileDto> GetProfileForEditAsync(Guid userId, string role)
        {
            var user = await _db.Users
                .Include(u => u.Student)
                .Include(u => u.Instructor)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new InvalidOperationException("User not found.");

            var dto = new EditProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Gender = user.Gender,
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

        public async Task<AuthResultDto> UpdateProfileAsync(Guid userId, string role, EditProfileDto dto, string webRootPath)
        {
            var user = await _db.Users
                .Include(u => u.Student)
                .Include(u => u.Instructor)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return AuthResultDto.Fail("User not found.");

            // ── 1. Update Core User Details ──────────────────────────────────
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Birthdate = dto.Birthdate;
            user.Gender = dto.Gender;
            user.Bio = dto.Bio;

            // ── 2. Handle Profile Picture Upload ─────────────────────────────
            if (dto.ProfilePicture != null && dto.ProfilePicture.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(dto.ProfilePicture.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    return AuthResultDto.Fail("Only JPG, JPEG, PNG, GIF, or WEBP images are allowed.");
                }

                // Create folder
                var uploadsDir = Path.Combine(webRootPath, "uploads", "profiles");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Remove old profile photo if exists
                if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                {
                    var oldPath = Path.Combine(webRootPath, user.ProfilePictureUrl.TrimStart('/'));
                    if (File.Exists(oldPath))
                    {
                        try { File.Delete(oldPath); } catch { /* Ignore delete failures */ }
                    }
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var fullPath = Path.Combine(uploadsDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ProfilePicture.CopyToAsync(stream);
                }

                user.ProfilePictureUrl = $"/uploads/profiles/{fileName}";
            }

            // ── 3. Update Role-Specific Details ──────────────────────────────
            if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                if (user.Student == null)
                {
                    user.Student = new Student { UserId = userId };
                    _db.Students.Add(user.Student);
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
                    _db.Instructors.Add(user.Instructor);
                }

                user.Instructor.Title = dto.Title;
                user.Instructor.GithubLink = dto.GithubLink;
                user.Instructor.LinkedInLink = dto.LinkedInLink;
            }

            var identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {
                var error = string.Join(" ", identityResult.Errors.Select(e => e.Description));
                return AuthResultDto.Fail(error);
            }

            // Save relationship entities changes if any
            await _db.SaveChangesAsync();

            return AuthResultDto.Ok("/Main", "Profile updated successfully!");
        }
    }
}
