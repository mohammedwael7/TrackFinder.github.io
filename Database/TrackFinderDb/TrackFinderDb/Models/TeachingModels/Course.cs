using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.TeachingModels
{
    public class Course
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public int StackId { get; set; }

        public Instructor Instructor { get; set; }
        public Stack Stack { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseSkill> CourseSkills { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Material> Materials { get; set; }
    }
}
