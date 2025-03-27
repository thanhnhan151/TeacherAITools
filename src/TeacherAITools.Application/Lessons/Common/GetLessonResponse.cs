namespace TeacherAITools.Application.Lessons.Common
{
    public class GetLessonResponse
    {
        public int LessonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public bool IsApproved { get; set; }
        public bool IsPublic { get; set; }
        public string LessonType { get; set; } = string.Empty;
        public string Requirement { get; set; } = string.Empty;
        public string SchoolSupply { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public int Week { get; set; }
        public string Module { get; set; } = string.Empty;
        public List<PeriodsResponse>? PeriodsResponses { get; set; }
    }

    public class PeriodsResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
    }
}
