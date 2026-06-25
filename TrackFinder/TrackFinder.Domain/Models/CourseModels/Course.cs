<<<<<<< HEAD:TrackFinder/TrackFinder.Domain/Models/CourseModels/Course.cs
﻿using TrackFinder.Domain.Models.UserModels;
=======
using TrackFinderDb.Models.ExamsAndQuizesModels;
using TrackFinderDb.Models.UserModels;
using TrackFinderDb.Models.AssessmentModels;
>>>>>>> 1b4528760a7f41b3c0d08094b24c1313c61af6be:Database/TrackFinderDb/TrackFinderDb/Models/CourseModels/Course.cs

namespace TrackFinder.Domain.Models.CourseModels
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
    public class CourseDuration
    {
        public int Value { get; set; }
        public DurationIn DurationIn { get; set; }
    }
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? ImageUrl { get; set; }
        public Language Language { get; set; }
        public Level Level { get; set; }
        public double Rating { get; set; }
        public CourseDuration Duration { get; set; }
        public double? Discount { get; set; }

        public Guid InstructorId { get; set; }
<<<<<<< HEAD:TrackFinder/TrackFinder.Domain/Models/CourseModels/Course.cs
        public virtual Instructor Instructor { get; set; } = null!;
        public virtual ICollection<Lesson> Lessons { get; set; } = null!;
        public virtual ICollection<CourseSkill> CourseSkills { get; set; } = null!;
=======
        public virtual Instructor Instructor { get; set; } = new Instructor();
        public int? TrackStackId { get; set; }
        public virtual TrackStack? TrackStack { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public virtual ICollection<CourseSkill> CourseSkills { get; set; } = new List<CourseSkill>();
>>>>>>> 1b4528760a7f41b3c0d08094b24c1313c61af6be:Database/TrackFinderDb/TrackFinderDb/Models/CourseModels/Course.cs
        public virtual ICollection<Material>? Materials { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
