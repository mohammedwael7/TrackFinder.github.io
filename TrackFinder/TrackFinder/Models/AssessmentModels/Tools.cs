using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinder.Models.AssessmentModels
{
    public class Tools
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<TrackStack>? RelatedTracks { get; set; } = new List<TrackStack>();
    }
}
