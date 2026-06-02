using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.CommunityAndPostsModels
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int? StudentId { get; set; }
        public int? InstructorId { get; set; }
        public int? ParentCommentId { get; set; }

        public Post Post { get; set; }
        public Student Student { get; set; }
        public Instructor Instructor { get; set; }
        public Comment ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; }
    }
}
