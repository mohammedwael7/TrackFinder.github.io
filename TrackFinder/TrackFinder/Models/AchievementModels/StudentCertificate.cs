using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrackFinder.Models.TeachingModels;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Models.AchievementModels
{
    public class StudentCertificate
    {
        public string CredentialsId { get; set; } = null!;

        public Guid CourseId { get; set; }
        public int CertificateId { get; set; }
        public Guid StudentId { get; set; }
        public virtual Course Course { get; set; } = new Course();
        public virtual Certificate Certificate { get; set; } = new Certificate();
        public virtual Student Student { get; set; } = new Student();
    }
}
