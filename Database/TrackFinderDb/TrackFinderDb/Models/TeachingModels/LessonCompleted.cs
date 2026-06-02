using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.TeachingModels
{
    public class LessonCompleted
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public int EnrollmentId { get; set; }

        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
