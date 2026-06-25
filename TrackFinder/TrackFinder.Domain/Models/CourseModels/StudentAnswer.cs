namespace TrackFinder.Domain.Models.CourseModels
{
    public class StudentAnswer
    {
        public Guid ExamAttepmtId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public virtual ExamAttempt ExamAttempt { get; set; }
        public virtual Question Question { get; set; }
        public virtual Option Answer { get; set; } = new Option();
    }
}
