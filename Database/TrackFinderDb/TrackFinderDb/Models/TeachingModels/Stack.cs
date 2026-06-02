using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.TrackModels;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.TeachingModels
{
    public class Stack
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Track> RelatedTracks { get; set; }
    }
}
