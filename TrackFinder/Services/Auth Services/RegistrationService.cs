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
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _db;
        private readonly IEncryptPasswordProvider _passwordProvider;
        private readonly ITokenProvider _tokenProvider;
        private readonly IEmailSender _emailSender;
        private readonly IMemoryCache _cache;
        private const int OtpExpiryMinutes = 10;

        public RegistrationService(
            AppDbContext db,
            IEncryptPasswordProvider passwordProvider,
            ITokenProvider tokenProvider,
            IEmailSender emailSender,
            IMemoryCache cache)
        {
            _db = db;
            _passwordProvider = passwordProvider;
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
            _cache = cache;
        }

        private static string CacheKey(string email) => $"otp:{email.ToLower()}";

        public async Task<AuthResultVM> RegisterAsync(RegisterVM dto, string webRootPath)
        {
            var existUserEmail = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existUserEmail is not null)
                return AuthResultVM.Fail("Email is already registered. Please use a different email.");

            var existUserName = await _db.Users.FirstOrDefaultAsync(u => u.UserName == dto.Username);
            if (existUserName is not null)
                return AuthResultVM.Fail("Username is already taken. Please choose a different username.");

            if (!Enum.TryParse<Gender>(dto.Gender, true, out var parsedGender))
                return AuthResultVM.Fail("Invalid gender value.");

            string? cvFilePath = null;
            bool isInstructor = string.Equals(dto.Role, "Instructor", StringComparison.OrdinalIgnoreCase);

            if (isInstructor)
            {
                if (dto.CvFile is null || dto.CvFile.Length == 0)
                    return AuthResultVM.Fail("A CV file (PDF) is required for instructor registration.");

                var extension = Path.GetExtension(dto.CvFile.FileName).ToLowerInvariant();
                if (extension != ".pdf")
                    return AuthResultVM.Fail("Only PDF files are accepted for your CV.");

                if (dto.CvFile.Length > 5 * 1024 * 1024)
                    return AuthResultVM.Fail("CV file must not exceed 5 MB.");

                var uploadsDir = Path.Combine(webRootPath, "uploads", "InstructorsCVs");
                Directory.CreateDirectory(uploadsDir);

                var fileName = $"{Guid.NewGuid()}{extension}";
                var fullPath = Path.Combine(uploadsDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                    await dto.CvFile.CopyToAsync(stream);

                cvFilePath = $"/uploads/InstructorsCVs/{fileName}";
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = dto.Username,
                NormalizedUserName = dto.Username.ToUpperInvariant(),
                Email = dto.Email,
                NormalizedEmail = dto.Email.ToUpperInvariant(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = parsedGender,
                Birthdate = dto.Birthdate,
                PasswordHash = _passwordProvider.HashPassword(dto.Password),
                // Identity's UserManager requires these to be non-null for any
                // later UserManager.UpdateAsync() call to succeed (e.g. from
                // UserProfileService when editing a profile). Since this user
                // is created via direct EF Core insert rather than
                // UserManager.CreateAsync(), we must generate them ourselves.
                SecurityStamp = Guid.NewGuid().ToString("N"),
                ConcurrencyStamp = Guid.NewGuid().ToString("N"),
                Role = isInstructor ? UserRole.Instructor : UserRole.Student,
                EmailConfirmed = false,
                IsBanned = false,
                CreatedAt = DateTime.UtcNow
            };

            _db.Users.Add(user);

            if (isInstructor)
            {
                _db.Instructors.Add(new Instructor
                {
                    UserId = user.Id,
                    CvFilePath = cvFilePath,
                    AdminApproved = false
                });
            }
            else
            {
                _db.Students.Add(new Student
                {
                    UserId = user.Id
                });
            }

            await _db.SaveChangesAsync();

            var otpCode = _tokenProvider.GenerateOtpCode();
            _cache.Set(CacheKey(dto.Email), otpCode, TimeSpan.FromMinutes(OtpExpiryMinutes));

            var emailBody = OtpMessage.Build(dto.FirstName, otpCode, OtpExpiryMinutes);
            _emailSender.SendEmail(dto.Email, emailBody, "Verify your Track Finder account");

            var encodedEmail = Uri.EscapeDataString(dto.Email);
            return AuthResultVM.Ok(
                $"/Login/VerifyOtp?email={encodedEmail}",
                "Registration successful! Please check your email for the verification code.");
        }
    }
}