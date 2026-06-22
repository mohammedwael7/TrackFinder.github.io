using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using TrackFinderDb.Models.AssessmentModels;
using TrackFinderDb.Models.CommunityModels;
using TrackFinderDb.Models.ExamsAndQuizesModels;
using TrackFinderDb.Models.TeachingModels;

namespace TrackFinderDb.Models.UserModels
{
    public class Instructor
    {
        public string? Title { get; set; }
        public string? GithubLink { get; set; } = string.Empty;
        public string? LinkedInLink { get; set; } = string.Empty;
        public double Rating { get; set; }

        [Key]
        public Guid UserId { get; set; }
        public Guid CommunityId { get; set; }
        public virtual User User { get; set; } = new User();
        public virtual Community? AdminstratedCommunity { get; set; }
        public virtual Community ModeratedCommunity { get; set; } = new Community();
        public ICollection<Course>? CreatedCourses { get; set; } = new List<Course>();
    }
}
