namespace TeacherAITools.Domain.Entities
{
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = string.Empty;

        // Foreign Key
        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;

        // Navigation
        public virtual ICollection<Ward> Wards { get; set; } = [];
    }
}
