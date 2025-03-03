namespace TeacherAITools.Domain.Entities
{
    public class TeachingTool
    {
        public int TeachingToolId { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}
