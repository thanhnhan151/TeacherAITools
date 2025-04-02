using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Notifications.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateNotificationCommand, Response<GetNotificationResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetNotificationResponse>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Title = request.createNotificationRequest.Title,
                Body = request.createNotificationRequest.Body,
                Time = request.createNotificationRequest.Time,
                IsRead = request.createNotificationRequest.IsRead,
                UserId = request.createNotificationRequest.UserId,
                NotificationTypeId = request.createNotificationRequest.NotificationTypeId,
            };

            var result = await _unitOfWork.Notifications.AddAsync(notification);

            await _unitOfWork.CompleteAsync();

            return new Response<GetNotificationResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}