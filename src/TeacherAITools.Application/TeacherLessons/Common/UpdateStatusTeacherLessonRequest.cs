using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Domain.Common;

namespace TeacherAITools.Application.TeacherLessons.Common
{
    public class UpdateStatusTeacherLessonRequest
    {
        public LessonStatus Status { get; set; } = LessonStatus.Draft;
        public string DisapprovedReason { get; set; } = string.Empty;
    }
}
