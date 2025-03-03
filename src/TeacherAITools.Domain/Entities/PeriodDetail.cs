namespace TeacherAITools.Domain.Entities
{
    public class PeriodDetail
    {
        public int Id { get; set; }
        public string StartUp { get; set; } = string.Empty;
        public string Knowledge { get; set; } = string.Empty;
        public string Practice { get; set; } = string.Empty;
        public string Apply { get; set; } = string.Empty;

        // Foreign Key
        public int PeriodId { get; set; }
        public virtual Period Period { get; set; } = null!;

        // Navigation
        public virtual ICollection<LessonHistory> LessonHistories { get; set; } = [];
    }
}
