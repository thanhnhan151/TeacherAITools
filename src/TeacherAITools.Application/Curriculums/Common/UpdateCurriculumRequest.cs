namespace TeacherAITools.Application.Curriculums.Common
{
    public class UpdateCurriculumRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SchoolYearId { get; set; }
    }
}