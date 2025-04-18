﻿using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IEmailService emailService) : IRequestHandler<CreateUserCommand, Response<GetUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEmailService _emailService = emailService;

        public async Task<Response<GetUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = [];
            var validator = new CreateUserCommandValidator(_unitOfWork);
            var result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                throw new ValidationException(ResponseCode.CREATED_UNSUCC, errorMessages);
            }

            var check = await _unitOfWork.Users.CheckSchoolManagerAsync(request.RoleId, request.GradeId, request.SchoolId);

            switch (check)
            {
                case 1:
                    errorMessages.Add(ResponseCode.MANAGER_HAS_EXISTED.GetDescription());
                    throw new ValidationException(ResponseCode.MANAGER_HAS_EXISTED, errorMessages);
                case 0:
                    errorMessages.Add(ResponseCode.VICE_MANAGER_HAS_EXISTED.GetDescription());
                    throw new ValidationException(ResponseCode.VICE_MANAGER_HAS_EXISTED, errorMessages);
                default:
                    break;
            }

            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = request.Password,
                Email = request.Email,
                RoleId = request.RoleId,
                SchoolId = request.SchoolId,
                GradeId = request.GradeId,
                IsActive = true,
                ManagerId = null
            };

            //if (newUser.RoleId != (int)AvailableRole.SubjectSpecialistManager)
            //{
            //    newUser.ManagerId = await _unitOfWork.Users.GetSchoolManagerAsync(request.GradeId, request.SchoolId);
            //}

            var mailRequest = new MailRequest
            {
                ToEmail = newUser.Email,
                Subject = "Welcome to AI Math Tool",
                Body = $"Username: {newUser.Username} Password: {newUser.PasswordHash}"
            };

            await _emailService.SendEmailAsync(mailRequest);

            var res = await _unitOfWork.Users.AddAsync(newUser);

            await _unitOfWork.CompleteAsync();

            return new Response<GetUserResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
