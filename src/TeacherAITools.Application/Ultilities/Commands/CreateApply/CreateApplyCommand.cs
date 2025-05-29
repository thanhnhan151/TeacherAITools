using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Ultilities.Commands.CreateApply
{
    public record CreateApplyCommand(
        string Goal,
        string TeacherActivities,
        string StudentActivities,
        int LessonId) : IRequest<Response<string>>;
}
