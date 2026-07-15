using TrackFinder.Models.UserModels;

namespace TrackFinder.Models.CourseModels
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
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public DateTime? CompletionDate { get; set; } = DateTime.Now;
        public DateTime? LastActiveDate { get; set; } = DateTime.Now;
        public int CompletedLessons { get; set; }

        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}