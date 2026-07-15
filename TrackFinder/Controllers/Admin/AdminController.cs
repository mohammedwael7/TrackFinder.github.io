using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Context;
using TrackFinder.Models.UserModels;
using TrackFinder.ViewModels.Admin_ViewModels;

namespace TrackFinder.Controllers.Admin
{
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

            var students = await _context.Users
                .CountAsync(x => x.Role == UserRole.Student);


            var instructors = await _context.Users
                .CountAsync(x => x.Role == UserRole.Instructor);


            var admins = await _context.Users
                .CountAsync(x => x.Role == UserRole.Admin);



            var model = new AdminDashboardVM
            {

                // Cards

                UsersCount = totalUsers,

                StudentsCount = students,

                InstructorsCount = instructors,


                PendingInstructors = await _context.Users
                    .CountAsync(x =>
                    x.Role == UserRole.Instructor &&
                    !x.EmailVerified),


                CoursesCount = await _context.Courses.CountAsync(),

                TracksCount = await _context.Tracks.CountAsync(),

                CommunitiesCount = await _context.Communities.CountAsync(),

                CertificatesCount = await _context.Certificates.CountAsync(),

                BadgesCount = await _context.Badges.CountAsync(),


                VerifiedEmails = await _context.Users
                    .CountAsync(x => x.EmailVerified),


                BannedUsers = await _context.Users
                    .CountAsync(x => x.IsBanned),



                // Chart Percentages

                StudentsPercentage =
                    totalUsers == 0 ? 0 :
                    students * 100 / totalUsers,


                InstructorPercentage =
                    totalUsers == 0 ? 0 :
                    instructors * 100 / totalUsers,


                AdminPercentage =
                    totalUsers == 0 ? 0 :
                    admins * 100 / totalUsers

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



            // Pending Instructor Requests

            model.InstructorRequests = await _context.Users
                .Include(x => x.Instructor)
                .Where(x =>
                    x.Role == UserRole.Instructor &&
                    !x.EmailVerified)
                .OrderByDescending(x => x.CreatedAt)
                .Take(5)
                .Select(x => new InstructorRequestDashboardVM
                {

                    Id = x.Id,

                    Name = x.FirstName + " " + x.LastName,

                    Email = x.Email,

                    Title = x.Instructor != null
                        ? x.Instructor.Title
                        : "-",

                    CreatedAt = x.CreatedAt

                })
                .ToListAsync();



            return View(
                "~/Views/Admin/Dashboard.cshtml",
                model
            );
        }
    }
}