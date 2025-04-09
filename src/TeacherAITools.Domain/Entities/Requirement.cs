namespace TeacherAITools.Domain.Entities
{
    public class Requirement
    {
        public int RequirementId { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation
        //public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}
