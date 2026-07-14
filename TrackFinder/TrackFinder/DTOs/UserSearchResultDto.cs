using System;

namespace TrackFinder.DTOs
{
    public class UserSearchResultDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
