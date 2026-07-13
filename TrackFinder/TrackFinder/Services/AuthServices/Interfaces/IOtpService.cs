using TrackFinder.DTOs.AuthenticationDTOs;

namespace TrackFinder.Services.AuthServices.Interfaces
{
    /// <summary>
    /// Handles email-verification OTP only.
    /// Password-reset OTP is handled by <see cref="IPasswordResetService"/>.
    /// </summary>
    public interface IOtpService
    {
        Task<AuthResultDto> VerifyOtpAsync(VerifyOtpDto dto);
        Task<AuthResultDto> ResendOtpAsync(string email);
    }
}
