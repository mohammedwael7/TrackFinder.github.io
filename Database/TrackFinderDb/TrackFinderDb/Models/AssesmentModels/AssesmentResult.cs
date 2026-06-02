using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TrackFinderDb.Models.AssesmentModels
{
    public class AssesmentResult
    {
        public int? AssesmentId { get; set; }
        public Collection<string>? ResultDetailsId { get; set; }

        public Assesment? Assesment { get; set; }
    }
   
}
