namespace TeacherAITools.Domain.Entities
{
    public class LessonHistory
    {
        public int Id { get; set; }
        public string StartUp { get; set; } = string.Empty;
        public string Knowledge { get; set; } = string.Empty;
        public string Practice { get; set; } = string.Empty;
        public string Apply { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }

        // Foreign Key
        //public int PeriodDetailId { get; set; }
        //public virtual PeriodDetail PeriodDetail { get; set; } = null!;

        public int TeacherLessonId { get; set; }
        public virtual TeacherLesson TeacherLesson { get; set; } = null!;
    }
}
