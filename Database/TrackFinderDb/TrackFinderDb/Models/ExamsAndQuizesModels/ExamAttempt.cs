using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.AssesmentModels;
using TrackFinderDb.Models.TeachingModels;

namespace TrackFinderDb.Models.ExamsAndQuizesModels
{
    public class ExamAttempt
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int EnrollmentId { get; set; }

        public Exam Exam { get; set; }
        public Enrollment Enrollment { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
        public AssessmentResult AssessmentResult { get; set; }
    }
}
