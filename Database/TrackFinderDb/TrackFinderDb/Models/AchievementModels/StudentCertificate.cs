using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrackFinderDb.Models.TeachingModels;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.AchievementModels
{
    public class StudentCertificate
    {
        public string CredentialsId { get; set; } = null!;

        public int CourseId { get; set; }
        public int CertificateId { get; set; }
        public Guid StudentId { get; set; }
        public virtual Course Course { get; set; } = new Course();
        public virtual Certificate Certificate { get; set; } = new Certificate();
        public virtual Student Student { get; set; } = new Student();
    }
}
