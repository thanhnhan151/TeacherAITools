namespace TeacherAITools.Domain.Entities
{
    public class Period
    {
        public int Id { get; set; }
        public int Number { get; set; }

        // Foreign Key
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;

        // Navigation
        public virtual ICollection<PeriodDetail> PeriodDetails { get; set; } = [];
    }
}
