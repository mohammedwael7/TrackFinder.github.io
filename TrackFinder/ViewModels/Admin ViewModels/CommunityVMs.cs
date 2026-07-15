using System.ComponentModel.DataAnnotations;

namespace TrackFinder.ViewModels.Communities
{
    public class CreateCommunityVM
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }

    public class EditCommunityVM : CreateCommunityVM
    {
        public Guid Id { get; set; }
    }

    public class CommunityListVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? AdminName { get; set; }

        public int MemberCount { get; set; }
    }

    public class CommunityDetailsVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? AdminName { get; set; }

        public int MemberCount { get; set; }
    }
}