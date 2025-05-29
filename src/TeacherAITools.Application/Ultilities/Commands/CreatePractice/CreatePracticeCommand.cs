using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Ultilities.Commands.CreatePractice
{
    public record CreatePracticeCommand(
        string Goal,
        string TeacherActivities,
        string StudentActivities,
        int LessonId) : IRequest<Response<string>>;
}
