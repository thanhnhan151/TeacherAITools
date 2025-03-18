namespace TeacherAITools.Application.Modules.Common{
    public class UpdateModuleRequest{
        public string Name { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
        public int Semester { get; set; }
        public int TotalPeriods { get; set; }
        public int CurriculumId { get; set; }
    }
}