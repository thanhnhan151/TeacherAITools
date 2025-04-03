using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
    string Username,
    string Password,
    string Email,
    int RoleId,
    int SchoolId,
    int GradeId) : IRequest<Response<GetUserResponse>>;
}
