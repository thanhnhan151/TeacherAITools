using MediatR;
using TeacherAITools.Application.Authentication.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<Response<AuthenticationResult>>;
}
