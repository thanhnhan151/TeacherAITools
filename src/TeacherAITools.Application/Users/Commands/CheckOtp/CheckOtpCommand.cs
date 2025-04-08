using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.CheckOtp
{
    public record CheckOtpCommand(
        string Otp,
        string NewPassword,
        string ConfirmedPassword) : IRequest<Response<string>>;
}
