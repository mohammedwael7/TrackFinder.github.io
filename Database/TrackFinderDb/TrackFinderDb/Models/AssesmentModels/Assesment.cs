using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.AssesmentModels
{
    internal class Assesment
    {
        public int AssesmentId { get; set; }
        public string? StudentId { get; set; }
        public int AssesmentResultId { get; set; }
        public ResultDetails? ResultDetails { get; set; }

        public DateTime SubmittedAt { get; set; }

        public Student? Student { get; set; }
        public AssesmentResult? AssesmentResult { get; set; }
    }
}
