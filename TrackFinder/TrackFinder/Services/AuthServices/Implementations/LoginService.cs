using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrackFinder.DTOs.AuthenticationDTOs;
using TrackFinder.Models.TrackFinderDbContext;
using TrackFinder.Models.UserModels;
using TrackFinder.Services.AuthServices.Interfaces;

namespace TrackFinder.Services.AuthServices.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _db;

        public LoginService(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<AuthResultDto> LoginAsync(LoginDto dto, HttpContext httpContext)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                return AuthResultDto.Fail("Invalid email or password.");

            if (user.IsBanned)
                return AuthResultDto.Fail("This account has been banned.");

            // ── 1. Verify email first ────────────────────────────────────────
            if (!user.EmailConfirmed)
                return AuthResultDto.UnverifiedEmail(user.Email!);

            // ── 2. Validate password ─────────────────────────────────────────
            var result = await _signInManager.PasswordSignInAsync(
                user, dto.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.IsLockedOut)
                return AuthResultDto.Fail("Too many failed attempts. Try again in 15 minutes.");

            if (!result.Succeeded)
                return AuthResultDto.Fail("Invalid email or password.");

            // ── 3. Admin approval gate — only after credentials are valid ────
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Instructor"))
            {
                var instructor = await _db.Instructors
                    .FirstOrDefaultAsync(i => i.UserId == user.Id);

                if (instructor == null || !instructor.AdminApproved)
                {
                    // Sign back out — password was correct but account not approved yet
                    await _signInManager.SignOutAsync();
                    return AuthResultDto.Fail("Your account is pending admin approval. You will be notified once approved.");
                }
            }

            return AuthResultDto.Ok("/Main");
        }

        public async Task LogoutAsync(HttpContext httpContext)
        {
            await _signInManager.SignOutAsync();
        }

        public Task<AuthResultDto> RefreshTokenAsync(HttpContext httpContext)
        {
            // Not needed anymore — cookie auth renews itself via SlidingExpiration.
            return Task.FromResult(AuthResultDto.Fail("Not used with cookie authentication."));
        }
    }
}