using MediatR;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateTeacherLesson
{
    public record UpdateTeacherLessonCommand(
        int Id,
        UpdateTeacherLessonRequest updateTeacherLessonRequest) : IRequest<Response<GetDetailTeacherLessonResponse>>;
}
