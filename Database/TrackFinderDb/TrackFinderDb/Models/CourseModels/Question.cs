using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.ExamsAndQuizesModels
{
    public enum QuestionType
    {
        MultipleChoice = 1,
        TrueFalse
    }
    public class Question
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int Points { get; set; }
        public int QuestionNumber { get; set; }
        public QuestionType QuestionType { get; set; } = QuestionType.MultipleChoice;

        public Exam Exam { get; set; } = new Exam();
        public virtual ICollection<Option>? Options { get; set; } = new List<Option>();
    }
}
