namespace TeacherAITools.Application.Modules.Common
{
    public class GetModuleResponse
    {
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
        public int Semester { get; set; }
        public int TotalPeriods { get; set; }
        public string Curriculum { get; set; } = string.Empty;
        public int GradeNumber { get; set; }
        public string Book { get; set; } = string.Empty;
    }
}
