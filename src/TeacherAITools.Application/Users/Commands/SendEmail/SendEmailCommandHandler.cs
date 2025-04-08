using MediatR;
using System.Security.Cryptography;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.SendEmail
{
    public class SendEmailCommandHandler(
        IUnitOfWork unitOfWork,
        IEmailService emailService) : IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEmailService _emailService = emailService;

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Users.SendOtpAsync(request.Email) ?? throw new ApiException(ResponseCode.EMAIL_NOT_FOUND);

            result.ResetPasswordOtp = GenerateRandomOtp();

            var mailRequest = new MailRequest
            {
                ToEmail = request.Email,
                Subject = "Reset Password OTP",
                Body = $"Reset Password OTP: {result.ResetPasswordOtp}"
            };

            await _emailService.SendEmailAsync(mailRequest);

            await _unitOfWork.Users.UpdateAsync(result);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.SUCCESS,
                data: $"An Otp code has been send to {request.Email}",
                message: ResponseCode.SUCCESS.GetDescription());
        }

        private static string GenerateRandomOtp()
        {
            return Convert.ToString(RandomNumberGenerator.GetInt32(100000, 1000000));
        }
    }
}
