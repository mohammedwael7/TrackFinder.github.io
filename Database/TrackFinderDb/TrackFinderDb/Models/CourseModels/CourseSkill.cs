using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.AssessmentModels;

namespace TrackFinderDb.Models.TeachingModels
{
    public class CourseSkill
    {
        public Guid CourseId { get; set; }
        public int GainedSkillId { get; set; }
        public Course Course { get; set; } = new Course();
        public GainedSkill GainedSkill { get; set; } = new GainedSkill();
    }
}
