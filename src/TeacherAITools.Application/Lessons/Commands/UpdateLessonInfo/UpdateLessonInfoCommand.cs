using MediatR;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateLessonInfo
{
    public record UpdateLessonInfoCommand(int LessonId, UpdateLessonInfoRequest Info) : IRequest<Response<GetLessonResponse>>;
}
