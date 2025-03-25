using MediatR;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateLesson
{
    public record UpdateLessonCommand(
        int Id,
        UpdateLessonRequest updateLessonRequest) : IRequest<Response<GetLessonResponse>>;
}
