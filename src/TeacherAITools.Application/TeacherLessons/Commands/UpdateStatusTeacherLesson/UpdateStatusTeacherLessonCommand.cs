using MediatR;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateStatusTeacherLesson
{
    public record UpdateStatusTeacherLessonCommand(
        int Id,
        UpdateStatusTeacherLessonRequest updateStatusTeacherLessonRequest) : IRequest<Response<GetDetailTeacherLessonResponse>>;
}
