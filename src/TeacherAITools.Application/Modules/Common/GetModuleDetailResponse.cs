namespace TeacherAITools.Application.Modules.Common
{
    public class GetModuleDetailResponse
    {
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GetLessonItem> Lessons { get; set; } = [];
    }
}
