namespace TeacherAITools.Application.Curriculums.Common{
    public class CreateCurriculumRequest{
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public int GradeId { get; set; }
        public int SchoolYearId { get; set; }
    }
}