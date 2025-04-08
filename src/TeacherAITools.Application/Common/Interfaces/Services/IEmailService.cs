using TeacherAITools.Application.Common.Models.Requests;

namespace TeacherAITools.Application.Common.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
