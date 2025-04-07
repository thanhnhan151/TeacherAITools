namespace TeacherAITools.Application.Grades.Common
{
    public class GetGradeDetailResponse
    {
        public int GradeId { get; set; }
        public int GradeNumber { get; set; }
        public List<GetModuleItem> Modules { get; set; } = [];
    }
}
