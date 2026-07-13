using System;
using System.Collections.Generic;
using System.Text;
using TrackFinder.Models.TeachingModels;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Models.ExamsAndQuizesModels
{
    public class Exam
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public int MaxAttempts { get; set; } = 3;
        public int DurationInMinutes { get; set; }
        public int TotalMarks { get; set; }
        public int PassMark { get; set; }

        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public virtual ICollection<ExamAttempt> ExamAttempts { get; set; } = new List<ExamAttempt>();
    }
}
