namespace TeacherAITools.Application.Curriculums.Common
{
    public class GetDetailCurriculumResponse
    {
        public int CurriculumId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public string Year { get; set; } = string.Empty;
        public List<DetailItem> CurriculumDetails { get; set; } = [];
        public List<ActivityItem> CurriculumActivities { get; set; } = [];
    }

    public class DetailItem
    {
        public int CurriculumDetailId { get; set; }
        public string CurriculumTopic { get; set; } = string.Empty;
        public string CurriculumSection { get; set; } = string.Empty;
        public string CurriculumSubSection { get; set; } = string.Empty;
        public string CurriculumContent { get; set; } = string.Empty;
        public string CurriculumGoal { get; set; } = string.Empty;
    }

    public class ActivityItem
    {
        public int CurriculumActivityId { get; set; }
        public string CurriculumAcitityDescription { get; set; } = string.Empty;
    }
}
