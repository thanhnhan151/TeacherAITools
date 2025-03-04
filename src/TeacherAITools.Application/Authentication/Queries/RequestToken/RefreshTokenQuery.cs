using MediatR;
using TeacherAITools.Application.Authentication.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Authentication.Queries.RequestToken
{
    public record RefreshTokenQuery(string RefreshToken) : IRequest<Response<AuthenticationResult>>;
}
