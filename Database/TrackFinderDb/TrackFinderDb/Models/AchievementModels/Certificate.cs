using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.AchievementModels
{
    public class Certificate
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CertificateUrl { get; set; }
    }
}
