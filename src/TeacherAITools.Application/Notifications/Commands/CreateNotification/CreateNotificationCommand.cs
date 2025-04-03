using MediatR;
using TeacherAITools.Application.Notifications.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Notifications.Commands.CreateNotification
{
    public record CreateNotificationCommand(
        CreateNotificationRequest createNotificationRequest) : IRequest<Response<GetNotificationResponse>>;
}