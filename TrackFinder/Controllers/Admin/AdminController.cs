using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Context;
using TrackFinder.Filters;
using TrackFinder.ViewModels.Admin_ViewModels;

namespace TrackFinder.Controllers.Admin
{
    [TypeFilter(typeof(AdminAuthorizationFilter))]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Dashboard()
        {

            var totalUsers = await _context.Users.CountAsync();

            var verifiedEmails = await _context.Users
                .CountAsync(x => x.EmailVerified);

            var pendingUsers = totalUsers - verifiedEmails;

            var instructorsCount = await _context.Instructors.CountAsync();

            var studentsCount = await _context.Students.CountAsync();

            var pendingInstructors = await _context.Instructors
                .CountAsync(x => !x.AdminApproved);

            var pendingStudents = await _context.Students
                .CountAsync(x => !x.AdminApproved);

            var model = new AdminDashboardVM
            {

                // Cards

                UsersCount = totalUsers,

                InstructorsCount = instructorsCount,

                StudentsCount = studentsCount,

                PendingInstructors = pendingInstructors,

                PendingStudents = pendingStudents,

                CoursesCount = await _context.Courses.CountAsync(),

                TracksCount = await _context.Tracks.CountAsync(),

                CommunitiesCount = await _context.Communities.CountAsync(),

                CertificatesCount = await _context.Certificates.CountAsync(),

                BadgesCount = await _context.Badges.CountAsync(),


                VerifiedEmails = verifiedEmails,

                BannedUsers = await _context.Users
                    .CountAsync(x => x.IsBanned),



                // Chart Percentages

                VerifiedPercentage =
                    totalUsers == 0 ? 0 :
                    verifiedEmails * 100 / totalUsers,

                PendingPercentage =
                    totalUsers == 0 ? 0 :
                    pendingUsers * 100 / totalUsers

            };



            // Latest Users

            model.LatestUsers = await _context.Users
                .OrderByDescending(x => x.CreatedAt)
                .Take(5)
                .Select(x => new DashboardUserVM
                {
                    Id = x.Id,

                    Name = x.FirstName + " " + x.LastName,

                    Email = x.Email,

                    Role = x.Role.ToString(),

                    CreatedAt = x.CreatedAt

                })
                .ToListAsync();



            // Pending User Requests

            model.PendingUserRequests = await _context.Users
                .Where(x => !x.EmailVerified)
                .OrderByDescending(x => x.CreatedAt)
                .Take(5)
                .Select(x => new PendingUserDashboardVM
                {

                    Id = x.Id,

                    Name = x.FirstName + " " + x.LastName,

                    Email = x.Email,

                    Role = x.Role.ToString(),

                    CreatedAt = x.CreatedAt

                })
                .ToListAsync();


            // Pending Instructor Requests (AdminApproved = false)

            model.PendingInstructorRequests = await _context.Instructors
                .Include(x => x.User)
                .Where(x => !x.AdminApproved)
                .OrderByDescending(x => x.User.CreatedAt)
                .Take(5)
                .Select(x => new PendingInstructorDashboardVM
                {
                    Id = x.UserId,

                    Name = x.User.FirstName + " " + x.User.LastName,

                    Email = x.User.Email,

                    Title = x.Title,

                    CreatedAt = x.User.CreatedAt
                })
                .ToListAsync();


            // Pending Student Requests (AdminApproved = false)

            model.PendingStudentRequests = await _context.Students
                .Include(x => x.User)
                .Where(x => !x.AdminApproved)
                .OrderByDescending(x => x.User.CreatedAt)
                .Take(5)
                .Select(x => new PendingStudentDashboardVM
                {
                    Id = x.UserId,

                    Name = x.User.FirstName + " " + x.User.LastName,

                    Email = x.User.Email,

                    CreatedAt = x.User.CreatedAt
                })
                .ToListAsync();



            return View(
                "~/Views/Admin/Dashboard.cshtml",
                model
            );
        }
    }
}