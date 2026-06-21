using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.CommunityModels
{
    public enum ReportReason
    {
        Spam,
        Harassment,
        OffensiveContent,
        FakeInformation,
        Other
    }
    public class PostReport
    {
        public Guid Id { get; set; }
        public ReportReason Reason { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid PostId { get; set; }
        public Guid ReporterId { get; set; }
        public virtual Post Post { get; set; } = null!;
        public virtual User Reporter { get; set; } = null!;
    }
}
