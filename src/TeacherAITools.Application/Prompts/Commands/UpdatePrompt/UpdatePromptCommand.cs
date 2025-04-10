using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Commands.UpdatePrompt
{
    public record UpdatePromptCommand(int Id, string Description) : IRequest<Response<string>>;
}
