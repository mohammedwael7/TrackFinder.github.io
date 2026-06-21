using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.TeachingModels
{
    public class Material
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;

        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public virtual Course? Course { get; set; }
    }
}
