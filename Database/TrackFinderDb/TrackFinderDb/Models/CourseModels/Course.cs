using TrackFinderDb.Models.ExamsAndQuizesModels;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.TeachingModels
{
    public enum Level
    {
        Beginner = 1,
        Intermediate,
        Advanced
    }
    public enum Language
    {
        English = 1,
        Arabic
    }
    public enum DurationIn
    {
        Days = 1,
        Weeks,
        Months
    }
    public struct CourseDuration
    {
        public int Value { get; set; }
        public DurationIn DurationIn { get; set; }
    }
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? ImageUrl { get; set; }
        public Language Language { get; set; }
        public Level Level { get; set; }
        public double Rating { get; set; }
        public CourseDuration Duration { get; set; }
        public double? Discount { get; set; }

        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; } = new Instructor();
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public virtual ICollection<CourseSkill> CourseSkills { get; set; } = new List<CourseSkill>();
        public virtual ICollection<Material>? Materials { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
