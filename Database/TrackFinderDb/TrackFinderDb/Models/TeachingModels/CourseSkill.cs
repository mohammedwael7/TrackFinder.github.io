using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.TeachingModels
{
    public class CourseSkill
    {
        public int Id { get; set; }
        public int CourseId { get; set; }

        public Course? Course { get; set; }
    }
}
