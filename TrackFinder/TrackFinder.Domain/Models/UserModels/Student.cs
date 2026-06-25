using System.ComponentModel.DataAnnotations;
using TrackFinder.Domain.Models.AchievementModels;
using TrackFinder.Domain.Models.AssessmentModels;
using TrackFinder.Domain.Models.CommunityModels;
using TrackFinder.Domain.Models.CourseModels;

namespace TrackFinder.Domain.Models.UserModels
{
    public class Student
    {
        public string? EducationState { get; set; }
        public string? SchoolOrUnversityName { get; set; }
        public string? Major { get; set; }
        public string? Minor { get; set; }
        public string? DegreeProgram { get; set; }
        public int AcademicYear { get; set; }
        public float GPA { get; set; }
        public string? Bio { get; set; }

        [Key]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<AssessmentResult>? AssessmentResults { get; set; }
        public virtual ICollection<StudentCertificate>? AchievedCertificates { get; set; }
        public virtual ICollection<JoinedMembers>? JoinedCommunities { get; set; }
    }
}
