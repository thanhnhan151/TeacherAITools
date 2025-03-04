using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        CreateUserRequest CreateUserRequest) : IRequest<Response<GetUserResponse>>;
}
