using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.CommunityAndPostsModels
{
    public class Community
    {
        public int Id { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
