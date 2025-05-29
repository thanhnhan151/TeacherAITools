using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Ultilities.Commands.CreateKnowLedge
{
    public record CreateKnowLedgeCommand(
        string Goal,
        string TeacherActivities,
        string StudentActivities,
        int LessonId) : IRequest<Response<string>>;
}
