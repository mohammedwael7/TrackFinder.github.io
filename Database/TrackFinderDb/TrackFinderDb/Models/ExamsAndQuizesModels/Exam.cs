using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.TeachingModels;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.ExamsAndQuizesModels
{
    public class Exam
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public int LessonId { get; set; }


        public Instructor Creator { get; set; }
        public Lesson Lesson { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<ExamAttempt> ExamAttempts { get; set; }
    }
}
