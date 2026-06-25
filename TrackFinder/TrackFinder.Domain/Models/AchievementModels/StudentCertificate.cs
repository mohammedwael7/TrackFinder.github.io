using TrackFinder.Domain.Models.CourseModels;
using TrackFinder.Domain.Models.UserModels;

namespace TrackFinder.Domain.Models.AchievementModels
{
    public class StudentCertificate
    {
        public string CredentialsId { get; set; } = null!;

        public Guid CourseId { get; set; }
        public int CertificateId { get; set; }
        public Guid StudentId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual Certificate Certificate { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
