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

        // Navigation
        public virtual ICollection<Comment> Comments { get; set; } = [];
    }
}
