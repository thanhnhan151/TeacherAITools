namespace TeacherAITools.Domain.Entities
{
    public class CurriculumTopic
    {
        public int CurriculumTopicId { get; set; }
        public string CurriculumTopicName { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<CurriculumSection> CurriculumSections { get; set; } = [];
    }
}
