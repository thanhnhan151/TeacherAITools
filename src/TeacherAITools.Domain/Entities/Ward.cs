namespace TeacherAITools.Domain.Entities
{
    public class Ward
    {
        public int WardId { get; set; }
        public string WardName { get; set; } = string.Empty;

        // Foreign Key
        public int DistrictId { get; set; }
        public virtual District District { get; set; } = null!;

        // Navigation
        public virtual ICollection<User> Users { get; set; } = [];
        public virtual ICollection<School> Schools { get; set; } = [];
    }
}
