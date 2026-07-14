using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrackFinder.DTOs;
using TrackFinder.Models.UserModels;
using TrackFinder.Services.UserProfileServices;

namespace TrackFinder.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        private readonly IUserProfileService _profileService;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;

        public MainController(
            IUserProfileService profileService,
            UserManager<User> userManager,
            IWebHostEnvironment env)
        {
            _profileService = profileService;
            _userManager = userManager;
            _env = env;
        }

        // ── GET /Main ─────────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdString = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                return RedirectToAction("Index", "Login");

            string role = User.IsInRole("Instructor") ? "Instructor" : "Student";
            var model = await _profileService.GetDashboardDataAsync(userId, role);

            ViewBag.ProfilePictureUrl = model.User?.ProfilePictureUrl;

            return View(model);
        }

        // ── GET /Main/Profile ──────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userIdString = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                return RedirectToAction("Index", "Login");

            string role = User.IsInRole("Instructor") ? "Instructor" : "Student";
            try
            {
                var dto = await _profileService.GetProfileForEditAsync(userId, role);
                ViewBag.ProfilePictureUrl = dto.ProfilePictureUrl;
                return View(dto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ── POST /Main/Profile ─────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(EditProfileDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProfilePictureUrl = dto.ProfilePictureUrl;
                return View(dto);
            }

            var userIdString = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                return RedirectToAction("Index", "Login");

            string role = User.IsInRole("Instructor") ? "Instructor" : "Student";
            var result = await _profileService.UpdateProfileAsync(userId, role, dto, _env.WebRootPath);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Failed to update profile.");
                ViewBag.ProfilePictureUrl = dto.ProfilePictureUrl;
                return View(dto);
            }

            TempData["SuccessMessage"] = result.SuccessMessage;
            return RedirectToAction(nameof(Index));
        }

        // ── GET /Main/Search?query= ──────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var userIdString = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                return RedirectToAction("Index", "Login");

            ViewBag.Query = query;
            var results = await _profileService.SearchUsersAsync(query ?? "", userId);
            return View(results);
        }

        // ── GET /Main/ViewProfile/{id} ────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> ViewProfile(Guid id)
        {
            var userIdString = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId))
                return RedirectToAction("Index", "Login");

            if (id == currentUserId)
                return RedirectToAction("Profile");

            try
            {
                var dto = await _profileService.GetPublicProfileAsync(id);
                ViewBag.ProfilePictureUrl = dto.ProfilePictureUrl;
                return View(dto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
