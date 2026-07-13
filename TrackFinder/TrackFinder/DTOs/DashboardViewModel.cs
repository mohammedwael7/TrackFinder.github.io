using System.Collections.Generic;
using TrackFinder.Models.AssessmentModels;
using TrackFinder.Models.CommunityModels;
using TrackFinder.Models.TeachingModels;
using TrackFinder.Models.UserModels;

namespace TrackFinder.DTOs
{
    public class DashboardViewModel
    {
        public User User { get; set; } = null!;
        public string Role { get; set; } = string.Empty;

        // ── Student Stats ─────────────────────────────────────────────
        public List<Enrollment> EnrolledCourses { get; set; } = new List<Enrollment>();
        public List<Track> RecommendedTracks { get; set; } = new List<Track>();

        // ── Instructor Stats ──────────────────────────────────────────
        public List<Course> CreatedCourses { get; set; } = new List<Course>();
        public int TotalLessonsCount { get; set; }
        public int TotalStudentsCount { get; set; }

        // ── Common Stats ──────────────────────────────────────────────
        public List<Post> RecentPosts { get; set; } = new List<Post>();
    }
}
