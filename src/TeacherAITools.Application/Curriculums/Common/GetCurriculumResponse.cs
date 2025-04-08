namespace TeacherAITools.Application.Curriculums.Common
{
    public class GetCurriculumResponse
    {
        public int CurriculumId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public string Year { get; set; } = string.Empty;
    }
}
