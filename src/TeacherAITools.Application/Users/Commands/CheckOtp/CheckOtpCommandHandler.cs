using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.CheckOtp
{
    public class CheckOtpCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<CheckOtpCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(CheckOtpCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = [];

            if (!request.NewPassword.Equals(request.ConfirmedPassword))
            {
                errorMessages.Add(ResponseCode.CONFIRM_PASSWORD_NOT_MATCH.GetDescription());
            }

            var result = await _unitOfWork.Users.ResetPasswordAsync(request.Otp);

            if (result == null)
            {
                errorMessages.Add(ResponseCode.INCORRECT_RESET_OTP.GetDescription());
                throw new ValidationException(ResponseCode.FAILED, errorMessages);
            }

            if (errorMessages.Count >= 1) throw new ValidationException(ResponseCode.FAILED, errorMessages);

            result.ResetPasswordOtp = null;
            result.PasswordHash = request.NewPassword;

            await _unitOfWork.Users.UpdateAsync(result);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.SUCCESS,
                data: $"Reset password successfully!",
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
