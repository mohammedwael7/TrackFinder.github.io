<<<<<<< HEAD:TrackFinder/TrackFinder.Domain/Models/CourseModels/StudentAnswer.cs
﻿namespace TrackFinder.Domain.Models.CourseModels
{
    public class StudentAnswer
    {
        public Guid ExamAttepmtId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public virtual ExamAttempt ExamAttempt { get; set; }
        public virtual Question Question { get; set; }
        public virtual Option Answer { get; set; } = new Option();
=======
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
>>>>>>> 1b4528760a7f41b3c0d08094b24c1313c61af6be:Database/TrackFinderDb/TrackFinderDb/Models/CourseModels/StudentAnswer.cs
    }
}
