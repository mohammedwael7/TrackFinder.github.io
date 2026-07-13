using System;
using System.Collections.Generic;
using System.Text;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Models.CommunityModels
{
    public class JoinedMembers
    {
        public Guid MemberId { get; set; }
        public Guid CommunityId { get; set; }
        public virtual Student Member { get; set; } = new Student();
        public virtual Community Community { get; set; } = new Community();
    }
}
