using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<ChangePasswordCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = [];
            var userQuery = await _unitOfWork.Users.GetAsync(u => u.PasswordHash.Equals(request.OldPassword));

            var user = userQuery.FirstOrDefault();

            if (user is null)
            {
                errorMessages.Add(ResponseCode.USER_NOT_FOUND.GetDescription());
                throw new ValidationException(ResponseCode.UPDATED_UNSUCC, errorMessages);
            }

            var validator = new ChangePasswordCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                throw new ValidationException(ResponseCode.UPDATED_UNSUCC, errorMessages);
            }

            user.PasswordHash = request.NewPassword;

            await _unitOfWork.Users.UpdateAsync(user);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.UPDATED_SUCCESS,
                message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
