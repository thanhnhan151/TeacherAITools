using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.SendEmail
{
    public record SendEmailCommand(string Email) : IRequest<Response<string>>;
}
