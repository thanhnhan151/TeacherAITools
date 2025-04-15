namespace TeacherAITools.Domain.Entities
{
    public class CurriculumDetail
    {
        public int CurriculumDetailId { get; set; }
        public string CurriculumContent { get; set; } = string.Empty;
        public string CurriculumGoal { get; set; } = string.Empty;

        // Foreign Key
        public int? CurriculumId { get; set; }
        public virtual Curriculum Curriculum { get; set; } = null!;

        public int? CurriculumSubSectionId { get; set; }
        public virtual CurriculumSubSection CurriculumSubSection { get; set; } = null!;
    }
}
