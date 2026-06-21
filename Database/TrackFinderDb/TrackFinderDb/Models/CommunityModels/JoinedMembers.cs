using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.CommunityModels
{
    public class JoinedMembers
    {
        public int MemberId { get; set; }
        public int CommunityId { get; set; }
        public virtual Student Member { get; set; } = new Student();
        public virtual Community Community { get; set; } = new Community();
    }
}
