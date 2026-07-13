using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using TrackFinder.DTOs.AuthenticationDTOs;
using TrackFinder.Models.UserModels;
using TrackFinder.Providers.Common.EmailService;
using TrackFinder.Providers.Common.EmailService.Messages;
using TrackFinder.Providers.Private.AuthProviders;
using TrackFinder.Services.AuthServices.Interfaces;

namespace TrackFinder.Services.AuthServices.Implementations
{
    /// <summary>
    /// Handles email-verification OTP only.
    /// Password-reset OTP is handled by <see cref="PasswordResetService"/>.
    /// </summary>
    public class OtpService : IOtpService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenProvider _tokenProvider;
        private readonly IEmailSender _emailSender;
        private readonly IMemoryCache _cache;
        private const int OtpExpiryMinutes = 10;

        private static string CacheKey(string email) => $"otp:{email.ToLower()}";

        public OtpService(
            UserManager<User> userManager,
            ITokenProvider tokenProvider,
            IEmailSender emailSender,
            IMemoryCache cache)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
            _cache = cache;
        }

        public async Task<AuthResultDto> VerifyOtpAsync(VerifyOtpDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                return AuthResultDto.Fail("Account not found.");

            if (!_cache.TryGetValue(CacheKey(dto.Email), out string? cachedOtp))
                return AuthResultDto.Fail("Your code has expired. Please request a new one.");

            if (cachedOtp != dto.OtpCode)
                return AuthResultDto.Fail("Invalid verification code.");

            user.EmailConfirmed = true;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return AuthResultDto.Fail("Something went wrong verifying your email.");

            _cache.Remove(CacheKey(dto.Email));

            bool isInstructor = await _userManager.IsInRoleAsync(user, "Instructor");

            return isInstructor
                ? AuthResultDto.Ok("/Login/PendingApproval",
                    "Email verified! Your account is now pending admin review.")
                : AuthResultDto.Ok("/Login",
                    "Email verified! You can now log in.");
        }

        public async Task<AuthResultDto> ResendOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return AuthResultDto.Fail("Account not found.");

            if (user.EmailConfirmed)
                return AuthResultDto.Fail("This email is already verified.");

            var otpCode = _tokenProvider.GenerateOtpCode();
            _cache.Set(CacheKey(email), otpCode, TimeSpan.FromMinutes(OtpExpiryMinutes));

            var emailBody = OtpMessage.Build(user.FirstName, otpCode, OtpExpiryMinutes);
            _emailSender.SendEmail(email, emailBody, "Verify your Track Finder account");

            return AuthResultDto.Ok(
                $"/Login/VerifyOtp?email={Uri.EscapeDataString(email)}",
                "A new verification code has been sent to your email.");
        }
    }
}