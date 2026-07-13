using System.ComponentModel.DataAnnotations;

namespace TrackFinder.DTOs.AuthenticationDTOs
{
    public class RegisterDto
    {
        // ── Role ─────────────────────────────────────────────────────────
        [Required(ErrorMessage = "Please select your role.")]
        public string Role { get; set; } = "Student"; // "Student" or "Instructor"

        // ── Personal Info ─────────────────────────────────────────────────
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9_\.]+$", ErrorMessage = "Username can only contain letters, numbers, underscores, and dots.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select your gender.")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime Birthdate { get; set; }

        // ── Instructor Only ────────────────────────────────────────────────
        /// <summary>
        /// CV file upload — required for Instructor role, must be a PDF.
        /// Validated in the service layer since IFormFile cannot be
        /// DataAnnotation-validated in a standard way.
        /// </summary>
        public IFormFile? CvFile { get; set; }
    }
}
