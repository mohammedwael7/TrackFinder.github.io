using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.TeachingModels
{
    public class Material
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int CourseId { get; set; }

        public Lesson Lesson { get; set; }
        public Course Course { get; set; }
    }
}
