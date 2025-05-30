namespace TeacherAITools.Application.Lessons.Common
{
    public class GetLessonInfoResponse
    {
        public int LessonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }

        public int LessonTypeId { get; set; }
        public string LessonType { get; set; } = string.Empty;

        public int NoteId { get; set; }
        public string Note { get; set; } = string.Empty;

        public int ModuleId { get; set; }
        public string Module { get; set; } = string.Empty;

        public int GradeNumber { get; set; }
    }
}
