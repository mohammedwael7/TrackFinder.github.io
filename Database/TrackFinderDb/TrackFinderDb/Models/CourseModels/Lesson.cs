using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.ExamsAndQuizesModels;

namespace TrackFinderDb.Models.TeachingModels
{
    public class LessonDuration
    {
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }
    public class Lesson
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public LessonDuration Duration { get; set; }
        public int Price { get; set; }
        public double? Discount { get; set; }

        public Guid? CourseId { get; set; }
        public virtual Course? Course { get; set; } = new Course();
        public virtual ICollection<Material>? Materials { get; set; } = new List<Material>();
        public virtual ICollection<Exam>? Exams { get; set; } = new List<Exam>();
    }
}
