namespace TeacherAITools.Domain.Entities
{
    public class CurriculumActivity
    {
        public int CurriculumActivityId { get; set; }
        public string CurriculumAcitityDescription { get; set; } = string.Empty;

        // Foreign Keys
        public int? CurriculumId { get; set; }
        public virtual Curriculum Curriculum { get; set; } = null!;
    }
}
