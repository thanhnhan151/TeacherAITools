namespace TeacherAITools.Application.TeacherLessons.Common
{
    public class GetDetailTeacherLessonResponse
    {
        public int TeacherLessonId { get; set; }
        public string StartUp { get; set; } = string.Empty;
        public string Knowledge { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public string SchoolSupply { get; set; } = string.Empty;
        public string Practice { get; set; } = string.Empty;
        public string Apply { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Lesson { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string DisapprovedReason { get; set; } = string.Empty;
        public int Grade { get; set; }
        public int TotalPeriods { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}
