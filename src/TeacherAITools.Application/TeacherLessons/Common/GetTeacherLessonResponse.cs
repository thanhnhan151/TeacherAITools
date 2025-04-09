namespace TeacherAITools.Application.TeacherLessons.Common
{
    public class GetTeacherLessonResponse
    {
        public int TeacherLessonId { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string LessonName { get; set; } = string.Empty;
        public string LessonType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
