using TrackFinder.Domain.Models.CourseModels;

namespace TrackFinder.Domain.Models.AssessmentModels
{
    public class GainedSkill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Track> AssessmentResults { get; set; } = null!;
        public virtual ICollection<Course>? Courses { get; set; } = null!;
    }
}
