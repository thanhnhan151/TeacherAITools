namespace TeacherAITools.Application.TeacherLessons.Common
{
    public class GetTeacherLessonResponse
    {
        public int TeacherLessonId { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Lesson { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public int Grade { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
