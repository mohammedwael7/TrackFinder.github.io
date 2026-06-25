<<<<<<< HEAD:TrackFinder/TrackFinder.Domain/Models/UserModels/Instructor.cs
﻿using System.ComponentModel.DataAnnotations;
using TrackFinder.Domain.Models.CommunityModels;
using TrackFinder.Domain.Models.CourseModels;
=======
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
>>>>>>> 1b4528760a7f41b3c0d08094b24c1313c61af6be:Database/TrackFinderDb/TrackFinderDb/Models/UserModels/Instructor.cs

namespace TrackFinder.Domain.Models.UserModels
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
        public virtual User User { get; set; } = null!;
        public virtual Community? AdminstratedCommunity { get; set; }
        public virtual Community ModeratedCommunity { get; set; } = null!;
        public ICollection<Course>? CreatedCourses { get; set; } = null!;
    }
}
