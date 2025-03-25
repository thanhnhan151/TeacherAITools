using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Response<GetUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userQuery = await _unitOfWork.Users.GetAsync(expression: u => u.UserId == request.Id, disableTracking: true);

            var user = userQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.USER_NOT_FOUND);

            user.Fullname = request.UpdateUserRequest.Fullname;
            user.Email = request.UpdateUserRequest.Email;
            user.PhoneNumber = request.UpdateUserRequest.PhoneNumber;
            user.DateOfBirth = request.UpdateUserRequest.DateOfBirth;
            user.Address = request.UpdateUserRequest.Address;

            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return new Response<GetUserResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
