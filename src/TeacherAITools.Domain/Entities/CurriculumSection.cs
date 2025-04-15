namespace TeacherAITools.Domain.Entities
{
    public class CurriculumSection
    {
        public int CurriculumSectionId { get; set; }
        public string CurriculumSectionName { get; set; } = string.Empty;

        // Foreign Keys
        public int? CurriculumTopicId { get; set; }
        public virtual CurriculumTopic CurriculumTopic { get; set; } = null!;

        // Navigation
        public virtual ICollection<CurriculumSubSection> CurriculumSubSections { get; set; } = [];
    }
}
