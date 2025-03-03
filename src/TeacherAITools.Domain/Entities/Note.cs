namespace TeacherAITools.Domain.Entities
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}
