using System;
using System.Collections.Generic;
using System.Text;
using TrackFinder.Models.TeachingModels;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Models.AssessmentModels
{
    public class TrackStack
    {
        public int Id { get; set; }
        public string StackName { get; set; } = string.Empty;
        public string StackDescription { get; set; } = null!;

        public int RelatedTrackId { get; set; }
        public virtual Track RelatedTrack { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
