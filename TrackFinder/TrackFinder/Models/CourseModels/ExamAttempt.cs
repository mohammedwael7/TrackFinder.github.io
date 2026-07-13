using System;
using System.Collections.Generic;
using System.Text;
using TrackFinder.Models.AssessmentModels;
using TrackFinder.Models.TeachingModels;

namespace TrackFinder.Models.ExamsAndQuizesModels
{
    public class ExamAttempt
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public int RemainingAttempts { get; set; }
        public DateTime AttemptDate { get; set; }

        public Guid ExamId { get; set; }
        public virtual Exam Exam { get; set; } = null!;
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    }
}
