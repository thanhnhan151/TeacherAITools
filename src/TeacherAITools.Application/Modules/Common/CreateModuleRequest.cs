namespace TeacherAITools.Application.Modules.Common
{
    public class CreateModuleRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
        public int Semester { get; set; }
        public int CurriculumId { get; set; }
        public int GradeId { get; set; }
        public int BookId { get; set; }
    }
}