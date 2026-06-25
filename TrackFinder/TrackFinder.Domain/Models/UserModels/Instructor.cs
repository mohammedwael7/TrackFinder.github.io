using System.ComponentModel.DataAnnotations;
using TrackFinder.Domain.Models.CommunityModels;
using TrackFinder.Domain.Models.CourseModels;

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
