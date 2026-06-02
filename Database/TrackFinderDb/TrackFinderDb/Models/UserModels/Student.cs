using Azure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using TrackFinderDb.Models.AchievementModels;
using TrackFinderDb.Models.TrackModels;

/*
 * ------------------------------------------------ *
 * Updated At: 2026-05-24                         |
 * Updated By: [Ahmed Ehab]                         |
 * ------------------------------------------------ *
-- ///////////////////////////////////////////////////////////////////
--                                                                  
-- ███████╗ ████████╗ ██╗   ██╗ ██████╗  ███████╗ ███╗   ██╗ ████████╗ 
-- ██╔════╝ ╚══██╔══╝ ██║   ██║ ██╔══██╗ ██╔════╝ ████╗  ██║ ╚══██╔══╝ 
-- ███████╗    ██║    ██║   ██║ ██║  ██║ █████╗   ██╔██╗ ██║    ██║    
-- ╚════██║    ██║    ██║   ██║ ██║  ██║ ██╔══╝   ██║╚██╗██║    ██║    
-- ███████║    ██║    ╚██████╔╝ ██████╔╝ ███████╗ ██║ ╚████║    ██║    
-- ╚══════╝    ╚═╝     ╚═════╝  ╚═════╝  ╚══════╝ ╚═╝  ╚═══╝    ╚═╝    
--                                                                  
-- ///////////////////////////////////////////////////////////////////

-- *-------------------------------------------------------*
--   STUDENT IS A CHILD TABLE FOR STUDENT-SPECIFIC DETAILS
-- *-------------------------------------------------------*

--    StudentId             - FK to [User] table, also serves as PK (1-to-1 relationship)
--    StudyTrackId          - FK to Track table, the track the student studying, optional (nullable if not specified yet)
--    EducationState        - Current education level (School, University, Graduated, Postgraduate, Ungraduated)
--    SchoolOrUnversityName - Name of the university attended, optional
--    Major                 - Student's major field of study, optional
--    Minor                 - Student's minor field of study, optional
--    DegreeProgram         - Type of degree program (e.g., Bachelor's, Master's, PhD), optional
--    AcademicYear          - Current year of study (e.g., 1, 2, 3, 4), optional
--    GPA                   - Grade Point Average on a 4.00 scale (e.g., 3.75), optional
--    Bio                   - Short self-description or about me text, optional
--    User                  - Navigation property to the related User entity (1-to-1 relationship)
--    StudyTrack            - Navigation property to the related Track entity representing the student's chosen track (many-to-one relationship)
*/



namespace TrackFinderDb.Models.UserModels
{
    public class Student
    {
        [Key]
        public string? StudentId { get; set; }
        public string? StudyTrackId { get; set; }

        public string? EducationState { get; set; }
        public string? SchoolOrUnversityName { get; set; }
        public string? Major { get; set; }
        public string? Minor { get; set; }
        public string? DegreeProgram { get; set; }
        public int AcademicYear { get; set; }
        public float GPA { get; set; }

        public string? Bio { get; set; }

        public User? User { get; set; }
        public Track? StudyTrack { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<AssessmentQuestion> AssessmentQuestions { get; set; }
        public ICollection<Certificate> AchievedCertificates { get; set; }
        public ICollection<Group> JoinedGroups { get; set; }
        public ICollection<Post> CreatedPosts { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
