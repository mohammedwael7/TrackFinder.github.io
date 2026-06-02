using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TrackFinderDb.Models.AssesmentModels;
using TrackFinderDb.Models.CommunityAndPostsModels;
using TrackFinderDb.Models.ExamsAndQuizesModels;
using TrackFinderDb.Models.TeachingModels;

namespace TrackFinderDb.Models.UserModels
{
    public class Instructor
    {

        public ICollection<Exam> CreatedExams { get; set; }
        public ICollection<Course> CreatedCourses { get; set; }
        public ICollection<Stack> CreatedStacks { get; set; }
        public ICollection<Group> CreatedGroups { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
