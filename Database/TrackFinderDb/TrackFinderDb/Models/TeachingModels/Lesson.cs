using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.TeachingModels
{
    public class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<LessonCompleted> CompletedBy { get; set; }
    }
}
