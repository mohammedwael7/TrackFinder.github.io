using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Models.CommunityModels
{
    public class Community
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public Guid AdminId { get; set; }
        public virtual Instructor Admin { get; set; } = null!;
        public virtual ICollection<JoinedMembers> JoinedMembers { get; set; } = new List<JoinedMembers>();
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Instructor>? Moderators { get; set; } = new List<Instructor>();
    }
}
