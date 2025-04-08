using System.ComponentModel;

namespace TeacherAITools.Domain.Common
{
    public enum LessonStatus
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Approved")]
        Approved = 1,
        [Description("Rejected")]
        Rejected = 2,
        [Description("Cancelled")]
        Cancelled = 3,
    }
}
