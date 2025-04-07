using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Commands
{
    public record CreatePromptCommand(int LessonId) : IRequest<Response<string>>;
}
