using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.DisableUser
{
    public class DisableUserCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<DisableUserCommand, Response<GetUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetUserResponse>> Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var userQuery = await _unitOfWork.Users.GetAsync(expression: u => u.UserId == request.UserId, disableTracking: true);

            var user = userQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.USER_NOT_FOUND);

            if (user.IsActive)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }

            await _unitOfWork.Users.UpdateAsync(user);

            await _unitOfWork.CompleteAsync();

            return new Response<GetUserResponse>(code: (int)ResponseCode.DISABLED_SUCCESS, message: ResponseCode.DISABLED_SUCCESS.GetDescription());
        }
    }
}
