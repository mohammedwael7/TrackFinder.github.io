using System.ComponentModel.DataAnnotations;

namespace TrackFinder.DTOs.AuthenticationDTOs
{
    public class ResetPasswordOtpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the 6-digit code.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be exactly 6 digits.")]
        public string OtpCode { get; set; } = string.Empty;
    }
}
