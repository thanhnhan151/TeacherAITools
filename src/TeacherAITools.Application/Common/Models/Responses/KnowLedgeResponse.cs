namespace TeacherAITools.Application.Common.Models.Responses
{
    public class KnowLedgeResponse
    {
        public int KnowLedgeId { get; set; }
        public string Goal { get; set; } = string.Empty;
        public string TeacherActivities { get; set; } = string.Empty;
        public string StudentActivities { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
    }
}
