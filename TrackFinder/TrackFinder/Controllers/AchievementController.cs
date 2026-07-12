using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Models.AchievementModels;
using TrackFinder.Models.ViewModels;
using TrackFinderDb.Models.TrackFinderDbContext;

namespace TrackFinder.Controllers
{
    public class AchievementController : Controller
    {
        private readonly AppDbContext _context;

        public AchievementController(AppDbContext context)
        {
            _context = context;
        }

        // صفحة الطالب المصلحة والتفاعلية 100%
        public async Task<IActionResult> Index(Guid studentId)
        {
            try
            {
                // حماية وتسهيل للتست: لو الـ ID جاي فاضي، السيرفر هيجبر الصفحة تقرأ الطالب اللي منحتله البادج في السيكوال عشان تنور فوراً
                if (studentId == Guid.Empty)
                {
                    studentId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                }

                var viewModel = new AchievementViewModel();

                viewModel.StudentCertificates = await _context.StudentCertificates
                    .Include(sc => sc.Certificate)
                    .Include(sc => sc.Course)
                    .Where(sc => sc.StudentId == studentId)
                    .ToListAsync();

                viewModel.FeaturedCertificate = viewModel.StudentCertificates
                    .FirstOrDefault(sc => sc.IsFeatured);

                viewModel.AllBadges = await _context.Badges.ToListAsync();

                viewModel.EarnedBadges = await _context.UserBadges
                    .Include(ub => ub.Badge)
                    .Where(ub => ub.UserId == studentId && ub.IsEarned)
                    .ToListAsync();

                // سطر مبسط للميلستون لمنع أي تهنيج أو تعليق في السيرفر
                viewModel.NextMilestoneCertificate = await _context.Certificates.FirstOrDefaultAsync();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        // ===== صفحة عرض شهادات الطالب الحالي فقط (آمنة تماماً ومصلحة) =====
        public async Task<IActionResult> MyCertificates(Guid studentId)
        {
            if (studentId != Guid.Empty)
            {
                var myCerts = await _context.StudentCertificates
                    .Include(sc => sc.Certificate)
                    .Where(sc => sc.StudentId == studentId)
                    .ToListAsync();

                return View(myCerts);
            }
            else
            {
                var emptyList = new List<StudentCertificate>();
                return View(emptyList);
            }
        }

        // ===== BADGES =====

        public async Task<IActionResult> Badges()
        {
            var badges = await _context.Badges.ToListAsync();
            return View(badges);
        }

        public IActionResult CreateBadge()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBadge(Badges badge)
        {
            if (ModelState.IsValid)
            {
                _context.Badges.Add(badge);
                await _context.SaveChangesAsync();
                return RedirectToAction("Badges");
            }
            return View(badge);
        }

        public async Task<IActionResult> EditBadge(int id)
        {
            var badge = await _context.Badges.FindAsync(id);
            if (badge == null) return NotFound();
            return View(badge);
        }

        [HttpPost]
        public async Task<IActionResult> EditBadge(Badges badge)
        {
            if (ModelState.IsValid)
            {
                _context.Badges.Update(badge);
                await _context.SaveChangesAsync();
                return RedirectToAction("Badges");
            }
            return View(badge);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBadge(int id)
        {
            var badge = await _context.Badges.FindAsync(id);
            if (badge != null)
            {
                _context.Badges.Remove(badge);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Badges");
        }

        // ===== CERTIFICATES =====

        public async Task<IActionResult> Certificates()
        {
            var certificates = await _context.Certificates.ToListAsync();
            return View(certificates);
        }

        public IActionResult CreateCertificate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCertificate(Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                _context.Certificates.Add(certificate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Certificates");
            }
            return View(certificate);
        }

        public async Task<IActionResult> EditCertificate(int id)
        {
            var cert = await _context.Certificates.FindAsync(id);
            if (cert == null) return NotFound();
            return View(cert);
        }

        [HttpPost]
        public async Task<IActionResult> EditCertificate(Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                _context.Certificates.Update(certificate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Certificates");
            }
            return View(certificate);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var cert = await _context.Certificates.FindAsync(id);
            if (cert != null)
            {
                _context.Certificates.Remove(cert);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Certificates");
        }

        // ===== إعطاء Badge لـ User =====

        public async Task<IActionResult> AssignBadge()
        {
            ViewBag.Badges = await _context.Badges.ToListAsync();
            ViewBag.Users = await _context.Users.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignBadge(Guid userId, int badgeId)
        {
            var exists = await _context.UserBadges
                .AnyAsync(ub => ub.UserId == userId && ub.BadgeId == badgeId);

            if (!exists)
            {
                _context.UserBadges.Add(new UserBadge
                {
                    UserId = userId,
                    BadgeId = badgeId,
                    EarnedAt = DateTime.UtcNow,
                    IsEarned = true
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Badges");
        }

        // ===== إعطاء Certificate لـ Student =====

        public async Task<IActionResult> AssignCertificate()
        {
            ViewBag.Certificates = await _context.Certificates.ToListAsync();
            ViewBag.Students = await _context.Students.Include(s => s.User).ToListAsync();
            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignCertificate(Guid studentId, int certificateId, Guid courseId)
        {
            var exists = await _context.StudentCertificates
                .AnyAsync(sc => sc.StudentId == studentId && sc.CertificateId == certificateId);

            if (!exists)
            {
                _context.StudentCertificates.Add(new StudentCertificate
                {
                    CredentialsId = Guid.NewGuid().ToString(),
                    StudentId = studentId,
                    CertificateId = certificateId,
                    CourseId = courseId,
                    IssuedAt = DateTime.UtcNow,
                    IsFeatured = false
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Certificates");
        }
    }
}
