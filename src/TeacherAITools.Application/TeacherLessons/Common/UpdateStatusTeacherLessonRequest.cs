using TeacherAITools.Domain.Common;

namespace TeacherAITools.Application.TeacherLessons.Common
{
    public class UpdateStatusTeacherLessonRequest
    {
        public LessonStatus Status { get; set; } = LessonStatus.Draft;
        public string DisapprovedReason { get; set; } = string.Empty;
    }
}
