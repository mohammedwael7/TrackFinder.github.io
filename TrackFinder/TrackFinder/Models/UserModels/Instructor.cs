using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using TrackFinder.Models.AssessmentModels;
using TrackFinder.Models.CommunityModels;
using TrackFinder.Models.ExamsAndQuizesModels;
using TrackFinder.Models.TeachingModels;

namespace TrackFinder.Models.UserModels
{
    public class Instructor
    {
        public string? Title { get; set; }
        public string? GithubLink { get; set; } = string.Empty;
        public string? LinkedInLink { get; set; } = string.Empty;
        public bool AdminApproved { get; set; } = false;
        public double Rating { get; set; }

        // ── Admin Approval ─────────────────────────────────────────
        /// <summary>Path to the instructor's uploaded CV (PDF only).</summary>
        public string? CvFilePath { get; set; }
        /// <summary>Set to true by an admin after reviewing the CV. Required before instructor can log in.</summary>
        public bool IsReviewed { get; set; } = false;

        [Key]
        public Guid UserId { get; set; }
        public Guid? CommunityId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Community? AdminstratedCommunity { get; set; }
        public virtual Community? ModeratedCommunity { get; set; }
        public ICollection<Course>? CreatedCourses { get; set; } = new List<Course>();
    }
}
