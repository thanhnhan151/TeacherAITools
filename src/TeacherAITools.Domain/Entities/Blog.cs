using TeacherAITools.Domain.Entities.Base.Implementations;

namespace TeacherAITools.Domain.Entities
{
    public class Blog : AuditableEntity
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string? Tags { get; set; } = string.Empty;

        // Foreign Key
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public int? TeacherLessonId { get; set; }
        public virtual TeacherLesson TeacherLesson { get; set; } = null!;

        // Navigation
        public virtual ICollection<Comment> Comments { get; set; } = [];
    }
}
