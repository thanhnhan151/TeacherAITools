using TeacherAITools.Application.Common.Models.Responses;

namespace TeacherAITools.Application.Lessons.Common
{
    public class GetLessonDetailResponse
    {
        public int LessonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public string LessonType { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public int GradeNumber { get; set; }
        public string SpecialAbility { get; set; } = string.Empty;
        public string GeneralCapacity { get; set; } = string.Empty;
        public string Quality { get; set; } = string.Empty;
        public string SchoolSupply { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public StartUpResponse StartUp { get; set; } = null!;
        public KnowLedgeResponse KnowLedge { get; set; } = null!;
        public PracticeResponse Practice { get; set; } = null!;
        public ApplyResponse Apply { get; set; } = null!;
    }

    //public class PeriodResponse
    //{
    //    public int Id { get; set; }
    //    public int Number { get; set; }
    //    public List<GetPeriodDetail> PeriodDetails { get; set; } = [];
    //}

    //public class GetPeriodDetail
    //{
    //    public int Id { get; set; }
    //    public string StartUp { get; set; } = string.Empty;
    //    public string Knowledge { get; set; } = string.Empty;
    //    public string Practice { get; set; } = string.Empty;
    //    public string Apply { get; set; } = string.Empty;
    //}
}
