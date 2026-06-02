using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.ExamsAndQuizesModels
{
    public class Option
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }

        public Question? Question { get; set; }
    }
}
