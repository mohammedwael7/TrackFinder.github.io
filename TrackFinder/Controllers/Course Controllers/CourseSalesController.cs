using Microsoft.AspNetCore.Mvc;
using TrackFinder.Context;
using TrackFinder.Models.CourseModels;
namespace TrackFinderDb.Controllers
{
    public class CourseController : Controller
    {

        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        public IActionResult Details(Guid id)
        {
            var item = _context.Courses.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);


        }


        public IActionResult MyCourses()
        {
            Guid userId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7");

            var myCourses = _context.Courses
                .Where(c => _context.Enrollments.Any(e =>
                    e.CourseId == c.Id && e.UserId == userId))
                .ToList();

            return View(myCourses);
        }

        [HttpGet]
        public IActionResult EnrollmentForm(Guid id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
                return NotFound();

            var enrollment = new Enrollment
            {
                CourseId = id
            };

            return View(enrollment);
        }


        public IActionResult EnrollCourse(Guid courseId)
        {
            var enrollment = new Enrollment
            {
                CourseId = courseId
            };

            return View("EnrollmentForm", enrollment);
        }

        [HttpPost]
        public IActionResult EnrollCourse(Enrollment model)
        {
            Guid userId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7");
            model.Course = null;
            model.User = null;
            var already = _context.Enrollments.Any(e =>
                e.CourseId == model.CourseId &&
                e.UserId == userId);

            if (already)
            {
                TempData["Error"] = "You are already enrolled in this course.";
                return RedirectToAction("Index");
            }

            model.UserId = userId;
            model.EnrollmentDate = DateTime.Now;
            model.Status = EnrollmentStatus.NotStarted;
            model.Progress = 0;
            model.CompletedLessons = 0;

            _context.Enrollments.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Enrollment completed successfully.";
            return RedirectToAction("MyCourses");
        }



    }
}
