using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TrackFinder.Context;
using TrackFinder.Models.UserModels;
using TrackFinder.Providers.Private.AuthProviders;
using TrackFinder.Services.AuthServices.Interfaces;
using TrackFinder.ViewModels.Auth_ViewModels;

namespace TrackFinder.Services.AuthServices.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext _db;
        private readonly IEncryptPasswordProvider _passwordProvider;

        public LoginService(AppDbContext db, IEncryptPasswordProvider passwordProvider)
        {
            _db = db;
            _passwordProvider = passwordProvider;
        }

        public async Task<AuthResultVM> LoginAsync(LoginVM dto, HttpContext httpContext)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user is null)
                return AuthResultVM.Fail("Invalid email or password.");

            if (user.IsBanned)
                return AuthResultVM.Fail("This account has been banned.");

            if (!user.EmailConfirmed)
                return AuthResultVM.UnverifiedEmail(user.Email);

            if (!_passwordProvider.VerifyPassword(dto.Password, user.PasswordHash))
                return AuthResultVM.Fail("Invalid email or password.");

            if (user.Role == UserRole.Instructor)
            {
                var instructor = await _db.Instructors.FirstOrDefaultAsync(i => i.UserId == user.Id);
                if (instructor == null || !instructor.AdminApproved)
                    return AuthResultVM.Fail("Your account is pending admin approval. You will be notified once approved.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120)
            });

            return AuthResultVM.Ok("/Main/Index");
        }

        public async Task LogoutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public Task<AuthResultVM> RefreshTokenAsync(HttpContext httpContext)
        {
            return Task.FromResult(AuthResultVM.Fail("Not used with cookie authentication."));
        }
    }
}