using MediatR;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.UpdateTeacherLesson
{
    public record UpdateTeacherLessonCommand(
        int Id,
        UpdateTeacherLessonRequest updateTeacherLessonRequest) : IRequest<Response<GetDetailTeacherLessonResponse>>;
}
