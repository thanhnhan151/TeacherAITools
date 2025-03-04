namespace TeacherAITools.Domain.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<District> Districts { get; set; } = [];
    }
}
