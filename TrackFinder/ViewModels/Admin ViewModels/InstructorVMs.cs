using System.ComponentModel.DataAnnotations;
using TrackFinder.Models.UserModels;

namespace TrackFinder.ViewModels.Instructors
{
    public class CreateInstructorVM
    {
        [Required]
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? GithubLink { get; set; }
        public string? LinkedInLink { get; set; }
        public double Rating { get; set; }
        [Required]
        public Guid CommunityId { get; set; }
    }

    public class EditInstructorVM : CreateInstructorVM
    {
        public Guid UserId { get; set; }
    }
    public class InstructorDetailsVM
    {
        public Guid UserId { get; set; }

        public string InstructorName { get; set; } = string.Empty;

        public string? Title { get; set; }

        public string? GithubLink { get; set; }

        public string? LinkedInLink { get; set; }

        public double Rating { get; set; }
    }
}
