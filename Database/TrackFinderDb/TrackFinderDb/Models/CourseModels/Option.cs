using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.ExamsAndQuizesModels
{
    public class Option
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public int OptionNumber { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; } = new Question();
    }
}
