using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.ExamsAndQuizesModels
{
    public class Question
    {
        public int Id { get; set; }
        public int ExamId { get; set; }

        public Exam Exam { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
