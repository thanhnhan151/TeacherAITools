namespace TeacherAITools.Domain.Entities
{
    public class CurriculumSubSection
    {
        public int CurriculumSubSectionId { get; set; }
        public string CurriculumSubSectionName { get; set; } = string.Empty;

        // Foreign Key
        public int? CurriculumSectionId { get; set; }
        public virtual CurriculumSection CurriculumSection { get; set; } = null!;

        // Navigation
        public virtual ICollection<CurriculumDetail> CurriculumDetails { get; set; } = [];
    }
}
