namespace TeacherAITools.Application.Lessons.Common
{
    public class UpdateIsApprovedRequest
    {
        public bool IsApproved { get; set; }
        public string DisapprovedReason { get; set; } = string.Empty;
    }
}
