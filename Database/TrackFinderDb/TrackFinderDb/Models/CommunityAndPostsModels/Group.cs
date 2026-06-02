using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.CommunityAndPostsModels
{
    public class Group
    {
        public int Id { get; set; }
        public int CommunityId { get; set; }

        public Community Community { get; set; }
        public ICollection<Student> Members { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
