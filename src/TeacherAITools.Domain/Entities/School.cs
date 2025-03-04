namespace TeacherAITools.Domain.Entities
{
    public class School
    {
        public int SchoolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool Status { get; set; }

        // Foreign Key
        public int WardId { get; set; }
        public virtual Ward Ward { get; set; } = null!;

        // Navigation
        public virtual ICollection<User> Users { get; set; } = [];
    }
}
