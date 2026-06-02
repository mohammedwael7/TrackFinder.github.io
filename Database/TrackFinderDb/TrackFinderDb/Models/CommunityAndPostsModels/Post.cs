using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.CommunityAndPostsModels
{
    public class Post
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }

        public Student Author { get; set; }
        public Group Group { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
