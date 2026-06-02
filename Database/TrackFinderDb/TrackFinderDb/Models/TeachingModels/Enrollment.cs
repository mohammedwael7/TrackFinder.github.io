using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.TeachingModels
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
        public ICollection<LessonCompleted> CompletedLessons { get; set; }
        public ICollection<ExamAttempt> ExamAttempts { get; set; }
    }
}
