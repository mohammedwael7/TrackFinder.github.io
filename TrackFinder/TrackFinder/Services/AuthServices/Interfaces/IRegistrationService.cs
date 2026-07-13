using TrackFinder.DTOs.AuthenticationDTOs;

namespace TrackFinder.Services.AuthServices.Interfaces
{
    /// <summary>
    /// Handles new user registration for both Students and Instructors.
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Registers a new user (Student or Instructor).
        /// On success, generates and emails an OTP for email verification,
        /// then returns a redirect to the OTP verification page.
        /// </summary>
        Task<AuthResultDto> RegisterAsync(RegisterDto dto, string webRootPath);
    }
}
