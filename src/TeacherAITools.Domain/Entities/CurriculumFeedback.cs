namespace TeacherAITools.Domain.Entities
{
    public class CurriculumFeedback
    {
        public int CurriculumFeedBackId { get; set; }
        public string Body { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = new DateTime(2025, 1, 1);

        // Foreign Keys
        public int? UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int? CurriculumId { get; set; }
        public virtual Curriculum Curriculum { get; set; } = null!;
    }
}
