namespace TeacherAITools.Application.Modules.Common
{
    public class GetLessonItem
    {
        public int LessonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LessonType { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public bool IsActive { get; set; }
    }
}
