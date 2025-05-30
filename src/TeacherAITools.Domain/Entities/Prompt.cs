namespace TeacherAITools.Domain.Entities
{
    public class Prompt
    {
        public int PromptId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(7);
        public string Description { get; set; } = string.Empty;

        // Foreign Keys
        public int? LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;

        public int? UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual StartUp? StartUp { get; set; }

        public virtual KnowLedge? KnowLedge { get; set; }

        public virtual Practice? Practice { get; set; }

        public virtual Apply? Apply { get; set; }

        // Navigation
        public virtual ICollection<TeacherLesson> LessonPlans { get; set; } = [];
    }
}
