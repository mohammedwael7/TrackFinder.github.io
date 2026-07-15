using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TrackFinder.Context;
using TrackFinder.Models.UserModels;
using TrackFinder.Providers.Common.EmailService;
using TrackFinder.Providers.Common.EmailService.Messages;
using TrackFinder.Providers.Private.AuthProviders;
using TrackFinder.Services.AuthServices.Interfaces;
using TrackFinder.ViewModels.Auth_ViewModels;

namespace TrackFinder.Services.AuthServices.Implementations
{
    public class OtpService : IOtpService
    {
        private readonly AppDbContext _db;
        private readonly ITokenProvider _tokenProvider;
        private readonly IEmailSender _emailSender;
        private readonly IMemoryCache _cache;
        private const int OtpExpiryMinutes = 10;

        private static string CacheKey(string email) => $"otp:{email.ToLower()}";

        public OtpService(
            AppDbContext db,
            ITokenProvider tokenProvider,
            IEmailSender emailSender,
            IMemoryCache cache)
        {
            _db = db;
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
            _cache = cache;
        }

        public async Task<AuthResultVM> VerifyOtpAsync(VerifyOtpVM dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user is null)
                return AuthResultVM.Fail("Account not found.");

            if (!_cache.TryGetValue(CacheKey(dto.Email), out string? cachedOtp))
                return AuthResultVM.Fail("Your code has expired. Please request a new one.");

            if (cachedOtp != dto.OtpCode)
                return AuthResultVM.Fail("Invalid verification code.");

            user.EmailConfirmed = true;
            await _db.SaveChangesAsync();
            _cache.Remove(CacheKey(dto.Email));

            return user.Role == UserRole.Instructor
                ? AuthResultVM.Ok("/Login/PendingApproval",
                    "Email verified! Your account is now pending admin review.")
                : AuthResultVM.Ok("/Login",
                    "Email verified! You can now log in.");
        }

        public async Task<AuthResultVM> ResendOtpAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
                return AuthResultVM.Fail("Account not found.");

            if (user.EmailConfirmed)
                return AuthResultVM.Fail("This email is already verified.");

            var otpCode = _tokenProvider.GenerateOtpCode();
            _cache.Set(CacheKey(email), otpCode, TimeSpan.FromMinutes(OtpExpiryMinutes));

            var emailBody = OtpMessage.Build(user.FirstName, otpCode, OtpExpiryMinutes);
            var emailSent = _emailSender.SendEmail(email, emailBody, "Verify your Track Finder account");

            if (!emailSent)
            {
                _cache.Remove(CacheKey(email));
                return AuthResultVM.Fail("Failed to send verification email. Please try again shortly.");
            }

            return AuthResultVM.Ok(
                $"/Login/VerifyOtp?email={Uri.EscapeDataString(email)}",
                "A new verification code has been sent to your email.");
        }
    }
}