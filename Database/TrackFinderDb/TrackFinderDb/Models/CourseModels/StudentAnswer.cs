using System;
using System.Collections.Generic;
using System.Text;

/*
 * ------------------------------------------------ *
 * Created At: 2026-06-22                           |
 * ------------------------------------------------ *
 *
 *    StudentAnswer stores one answer row per question
 *    inside a single ExamAttempt.
 *
 *    StudentAnswerId  - Surrogate PK, auto-incremented
 *    ExamAttemptId    - FK → ExamAttempt (which sitting of the exam)
 *    QuestionId       - FK → Question    (which question was answered)
 *    SelectedOptionId - FK → Option      (which option the student picked)
 *                       nullable = question was skipped
 *    IsCorrect        - Computed/stored flag for quick scoring
 */

namespace TrackFinderDb.Models.ExamsAndQuizesModels
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }

        public int ExamAttemptId { get; set; }
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }       // nullable → question skipped

        public virtual ExamAttempt ExamAttempt { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
        public virtual Option? SelectedOption { get; set; }
    }
}
