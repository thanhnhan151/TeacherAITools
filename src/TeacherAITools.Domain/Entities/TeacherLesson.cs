using TeacherAITools.Domain.Common;

namespace TeacherAITools.Domain.Entities
{
    public class TeacherLesson
    {
        public int TeacherLessonId { get; set; }
        public string StartUp { get; set; } = string.Empty;
        public string Knowledge { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public string SchoolSupply { get; set; } = string.Empty;
        public string Practice { get; set; } = string.Empty;
        public string Apply { get; set; } = string.Empty;
        public LessonStatus Status { get; set; } = LessonStatus.Draft;
        public int RejectedCount { get; set; } = 0;
        public string DisapprovedReason { get; set; } = string.Empty;

        // Foreign keys
        public int? PromptId { get; set; }
        public virtual Prompt Prompt { get; set; } = null!;

        public int? UserId { get; set; }
        public virtual User User { get; set; } = null!;

        // Navigation
        public virtual ICollection<LessonHistory> LessonHistories { get; set; } = [];

        public virtual ICollection<Blog> Blogs { get; set; } = [];
    }
}
