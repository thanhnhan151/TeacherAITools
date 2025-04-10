using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(
        string UsernameOrEmail,
        string NewPassword,
        string ConfirmedPassword) : IRequest<Response<string>>;
}
