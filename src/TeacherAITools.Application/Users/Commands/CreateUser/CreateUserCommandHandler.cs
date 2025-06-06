﻿using MediatR;
using TeacherAITools.Application.Common.Constants;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Common;
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
            var newUser = new User();
            var mailRequest = new MailRequest();
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

            if (request.RoleId == (int)AvailableRole.SubjectSpecialistManager)
            {
                var check = await _unitOfWork.Users.CheckGradeManagerAsync(request.RoleId, request.GradeId);

                if (check == 1)
                {
                    errorMessages.Add(ResponseCode.MANAGER_HAS_EXISTED.GetDescription());
                    throw new ValidationException(ResponseCode.MANAGER_HAS_EXISTED, errorMessages);
                }

                newUser.Username = request.Username;
                newUser.PasswordHash = request.Password;
                newUser.Email = request.Email;
                newUser.RoleId = request.RoleId;
                newUser.SchoolId = request.SchoolId;
                newUser.GradeId = request.GradeId;
                newUser.ImgURL = DefaultAvatar.UserPicture;
                newUser.IsActive = true;
                newUser.ManagerId = null;

                var temp = await _unitOfWork.Users.AddAsync(newUser);

                await _unitOfWork.CompleteAsync();

                mailRequest.ToEmail = newUser.Email;
                mailRequest.Subject = "TÀI KHOẢN ĐĂNG NHẬP PHẦN MỀM";
                mailRequest.Body = "Chào Quản lý chuyên môn lớp: " + $"{newUser.GradeId}" + "\n\nHệ thống gửi tài khoản để  đăng nhập phần mềm:\n\nUsername: " + $"{newUser.Username}" + " Hãy giữ kín chi tiết này.\n\nPassword: " + $"{newUser.PasswordHash}" + " Nhập đúng ký tự hoa/thường\n\nTrân trọng,\n\nHệ thống Math AI Tools";

                await _emailService.SendEmailAsync(mailRequest);

                return new Response<GetUserResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
            }

            var managerId = await _unitOfWork.Users.GetSchoolManagerAsync(request.GradeId);

            if (managerId == 0)
            {
                errorMessages.Add(ResponseCode.MANAGER_HAS_EXISTED.GetDescription());
                throw new ValidationException(ResponseCode.MANAGER_NOT_FOUND, errorMessages);
            }

            newUser.Username = request.Username;
            newUser.PasswordHash = request.Password;
            newUser.Email = request.Email;
            newUser.RoleId = request.RoleId;
            newUser.SchoolId = request.SchoolId;
            newUser.GradeId = request.GradeId;
            newUser.ImgURL = DefaultAvatar.UserPicture;
            newUser.IsActive = true;
            newUser.ManagerId = managerId;

            var newResult = await _unitOfWork.Users.AddAsync(newUser);

            await _unitOfWork.CompleteAsync();

            mailRequest.ToEmail = newUser.Email;
            mailRequest.Subject = "TÀI KHOẢN ĐĂNG NHẬP PHẦN MỀM";
            mailRequest.Body = "Chào Giáo viên lớp: " + $"{newUser.GradeId}" + ",\r\n\r\nHệ thống gửi tài khoản để  đăng nhập phần mềm:\r\n\r\nUsername\r\n\r\n" + $"{newUser.Username}" + "\r\n\r\nHãy giữ kín việc này\r\n\r\nPassword\r\n\r\n" + $"{newUser.PasswordHash}" + "\r\n\r\nNhập đúng ký tự hoa/thường\r\n\r\nTrân trọng,\r\n\r\nHệ thống Math AI Tools";

            await _emailService.SendEmailAsync(mailRequest);

            return new Response<GetUserResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
