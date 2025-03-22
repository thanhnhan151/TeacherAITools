namespace TeacherAITools.Domain.Entities
{
    public class SchoolSupply
    {
        public int SchoolSupplyId { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}
