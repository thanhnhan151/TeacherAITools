using TeacherAITools.Domain.Common;

namespace TeacherAITools.Domain.Entities
{
    public class Period
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public LessonStatus Status { get; set; } = LessonStatus.Pending;

        // Foreign Key
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;

        public int? UserId { get; set; }
        public virtual User User { get; set; } = null!;

        // Navigation
        public virtual ICollection<PeriodDetail> PeriodDetails { get; set; } = [];
    }
}
