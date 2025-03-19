using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
    string Username,
    string Password,
    string Fullname,
    string Email,
    string PhoneNumber,
    DateOnly DateOfBirth,
    Gender Gender,
    string Address) : IRequest<Response<GetUserResponse>>;
}
