namespace TeacherAITools.Application.Periods.Common
{
    public class CreatePeriodRequest
    {
        public int Number { get; set; }
        public int LessonId { get; set; }
        public string StartUp { get; set; } = string.Empty;
        public string Knowledge { get; set; } = string.Empty;
        public string Practice { get; set; } = string.Empty;
        public string Apply { get; set; } = string.Empty;
    }
}