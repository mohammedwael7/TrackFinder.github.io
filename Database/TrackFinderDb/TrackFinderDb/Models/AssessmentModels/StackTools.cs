using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.AssessmentModels
{
    public class StackTools
    {
        public int StackId { get; set; }
        public int ToolId { get; set; }
        public virtual TrackStack RelatedStack { get; set; } = null!;
        public virtual Tools RelatedTool { get; set; } = null!;
    }
}
