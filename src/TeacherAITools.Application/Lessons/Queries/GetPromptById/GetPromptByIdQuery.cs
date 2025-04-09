using MediatR;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetPromptById
{
    public record GetPromptByIdQuery(int Id) : IRequest<Response<GetPromptResponse>>;
}
