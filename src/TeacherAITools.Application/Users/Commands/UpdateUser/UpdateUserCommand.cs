using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
        int Id,
        UpdateUserRequest UpdateUserRequest) : IRequest<Response<GetUserResponse>>;
}
