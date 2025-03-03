using TeacherAITools.Domain.Entities.Base.Interfaces;

namespace TeacherAITools.Domain.Entities.Base.Implementations
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
