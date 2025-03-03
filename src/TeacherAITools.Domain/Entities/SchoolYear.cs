namespace TeacherAITools.Domain.Entities
{
    public class SchoolYear
    {
        public int SchoolYearId { get; set; }
        public string Year { get; set; } = string.Empty;

        public virtual ICollection<Curriculum> Curriculums { get; set; } = [];
    }
}
