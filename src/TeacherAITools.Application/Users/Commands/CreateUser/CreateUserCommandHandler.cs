using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Response<GetUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userQuery = await _unitOfWork.Users.GetAsync(
                user => user.Email.ToLower().Equals(request.Email.ToLower()) ||
                        user.Username.ToLower().Equals(request.Email.ToLower()));

            if (userQuery.FirstOrDefault() is not null) throw new ApiException(ResponseCode.USERNAME_EMAIL_ERR);

            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = request.Password,
                Fullname = request.Fullname,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                RoleId = request.RoleId,
                SchoolId = request.SchoolId
            };

            var result = await _unitOfWork.Users.AddAsync(newUser);

            await _unitOfWork.CompleteAsync();

            return new Response<GetUserResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
