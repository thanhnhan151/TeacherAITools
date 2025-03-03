namespace TeacherAITools.Domain.Entities
{
    public class LessonType
    {
        public int LessonTypeId { get; set; }
        public string LessonTypeName { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}
