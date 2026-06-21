using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.AssessmentModels
{
    public class AssessmentResultTracks
    {
        public int SimilarityScore { get; set; }

        public int AssessmentResultId { get; set; }
        public int TrackId { get; set; }
        public virtual AssessmentResult AssessmentResult { get; set; } = null!;
        public virtual Track Track { get; set; } = null!;
    }
}
