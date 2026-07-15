using System.ComponentModel.DataAnnotations;
using TrackFinder.Models.CourseModels;
using TrackFinder.Models.UserModels;

namespace TrackFinderDb.Models.TeachingModels
{
    public enum EnrollmentStatus
    {
        NotStarted = 1,
        InProgress,
        Completed,
        Dropped
    }
    public class Enrollment
    {
        public int Progress { get; set; }
        public EnrollmentStatus Status { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public int CompletedLessons { get; set; }

        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public virtual Course Course { get; set; } = new Course();
        public virtual User User { get; set; } = new User();

        [Required(ErrorMessage = "Card Number is required")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Card Number  must be 14 digits")]
        public string? CardNumber { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime? ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be 3 digits")]
        public string? CVV { get; set; }
    }
}