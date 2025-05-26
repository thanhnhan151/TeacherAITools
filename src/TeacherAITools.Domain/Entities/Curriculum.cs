using TeacherAITools.Domain.Entities.Base.Implementations;

namespace TeacherAITools.Domain.Entities
{
    public class Curriculum : AuditableEntity
    {
        public int CurriculumId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }

        public int SchoolYearId { get; set; }
        public virtual SchoolYear SchoolYear { get; set; } = null!;

        public int? GradeId { get; set; }
        public virtual Grade Grade { get; set; } = null!;

        // Navigation
        public virtual ICollection<Module> Modules { get; set; } = [];
        public virtual ICollection<CurriculumDetail> CurriculumDetails { get; set; } = [];
        public virtual ICollection<CurriculumActivity> CurriculumActivities { get; set; } = [];
        public virtual ICollection<CurriculumFeedback> CurriculumFeedbacks { get; set; } = [];
    }
}
