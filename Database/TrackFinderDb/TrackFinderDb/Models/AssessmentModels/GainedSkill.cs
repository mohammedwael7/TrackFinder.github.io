using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.TeachingModels;

namespace TrackFinderDb.Models.AssessmentModels
{
    public class GainedSkill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Track> AssessmentResults { get; set; } = new List<Track>();
        public virtual ICollection<Course>? Courses { get; set; } = new List<Course>();
    }
}
