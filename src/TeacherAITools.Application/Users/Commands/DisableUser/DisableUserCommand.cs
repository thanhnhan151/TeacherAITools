using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.DisableUser
{
    public record DisableUserCommand(int UserId) : IRequest<Response<GetUserResponse>>;
}
