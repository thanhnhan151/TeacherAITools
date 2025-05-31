using MediatR;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetPromptsByUserId
{
    public record GetPromptsByUserIdQuery(int UserId) : IRequest<Response<List<GetPromptResponse>>>;
}
