using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Ultilities.Commands.CreateStartUp
{
    public record CreateStartUpCommand(
        string Goal,
        string TeacherActivities,
        string StudentActivities,
        int LessonId) : IRequest<Response<string>>;
}
