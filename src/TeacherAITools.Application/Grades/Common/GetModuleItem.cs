namespace TeacherAITools.Application.Grades.Common
{
    public class GetModuleItem
    {
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public bool IsActive { get; set; }
    }
}
