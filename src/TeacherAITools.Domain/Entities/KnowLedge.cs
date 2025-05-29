namespace TeacherAITools.Domain.Entities
{
    public class KnowLedge
    {
        public int KnowLedgeId { get; set; }
        public string Goal { get; set; } = string.Empty;
        public string TeacherActivities { get; set; } = string.Empty;
        public string StudentActivities { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;

        // Foreign Keys
        public int? LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;

        public int? PromptId { get; set; }
        public virtual Prompt Prompt { get; set; } = null!;
    }
}
