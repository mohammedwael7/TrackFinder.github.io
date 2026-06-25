namespace TrackFinder.Domain.Models.CourseModels
{
    public class Option
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string OptionText { get; set; } = string.Empty;
        public int OptionNumber { get; set; }

        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; } = null!;
    }
}
