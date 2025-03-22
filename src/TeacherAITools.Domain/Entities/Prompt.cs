namespace TeacherAITools.Domain.Entities
{
    public class Prompt
    {
        public int PromptId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;
    }
}
