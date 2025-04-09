namespace TeacherAITools.Application.Lessons.Common
{
    public class UpdateLessonRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public int LessonTypeId { get; set; }
        //public int RequirementId { get; set; }
        //public int SchoolSupplyId { get; set; }
        public int NoteId { get; set; }
        //public int UserId { get; set; }
        public int WeekId { get; set; }
        public int ModuleId { get; set; }
    }
}