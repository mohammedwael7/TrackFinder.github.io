using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.ExamsAndQuizesModels
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public int ExamAttemptId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }

        public ExamAttempt? ExamAttempt { get; set; }
    }
}
