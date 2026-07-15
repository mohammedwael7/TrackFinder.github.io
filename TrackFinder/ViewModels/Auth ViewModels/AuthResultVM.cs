
namespace TrackFinder.ViewModels.Auth_ViewModels
{
    /// <summary>
    /// Unified return object from all Auth service methods.
    /// Controllers stay thin — they just check Success and act on RedirectUrl or ErrorMessage.
    /// </summary>
    public class AuthResultVM
    {
        public bool Success { get; set; }

        /// <summary>
        /// Human-readable error message shown to the user on failure.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Where to redirect after a successful operation.
        /// </summary>
        public string? RedirectUrl { get; set; }

        /// <summary>
        /// Optional success/info message shown to the user (e.g. "Email verified!").
        /// </summary>
        public string? SuccessMessage { get; set; }

        /// <summary>
        /// True when the login failed specifically because the user's email is not yet verified.
        /// Controllers should redirect to the OTP page rather than showing a generic error.
        /// </summary>
        public bool RequiresEmailVerification { get; set; }

        /// <summary>
        /// The email address of the user who needs to verify their email.
        /// Populated only when RequiresEmailVerification is true.
        /// </summary>
        public string? Email { get; set; }

        // ── Factory helpers ──────────────────────────────────────────────

        public static AuthResultVM Ok(string redirectUrl, string? message = null)
            => new() { Success = true, RedirectUrl = redirectUrl, SuccessMessage = message };

        public static AuthResultVM Fail(string error)
            => new() { Success = false, ErrorMessage = error };

        /// <summary>
        /// Used when login fails because the email is not yet verified.
        /// Carries the email so the controller can redirect straight to the OTP page.
        /// </summary>
        public static AuthResultVM UnverifiedEmail(string email)
            => new() { Success = false, RequiresEmailVerification = true, Email = email };
    }
}
