﻿namespace TeacherAITools.Application.TeacherLessons.Common
{
    public class GetTeacherLessonResponse
    {
        public int TeacherLessonId { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Lesson { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
