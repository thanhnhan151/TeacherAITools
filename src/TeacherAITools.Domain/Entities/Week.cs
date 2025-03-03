namespace TeacherAITools.Domain.Entities
{
    public class Week
    {
        public int WeekId { get; set; }
        public int WeekNumber { get; set; }

        // Navigation
        public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}
