using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Commands.CreatePrompt
{
    public record CreatePromptCommand(
        int LessonId,
        string Description) : IRequest<Response<string>>;
}
