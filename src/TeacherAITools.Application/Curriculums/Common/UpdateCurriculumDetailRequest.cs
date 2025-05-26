namespace TeacherAITools.Application.Curriculums.Common
{
    public class UpdateCurriculumDetailRequest
    {
        public string CurriculumContent { get; set; } = string.Empty;
        public string CurriculumGoal { get; set; } = string.Empty;
        public int? CurriculumId { get; set; }
        public int? CurriculumSubSectionId { get; set; }
    }
}