namespace TeacherAITools.Application.Curriculums.Common
{
    public class GetCurriculumFeedbackResponse
    {
        public int CurriculumFeedbackId { get; set; }
        public string ImgURL { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string TimeStamp { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
    }
}
