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
            var validator = new CreateUserCommandValidator(_unitOfWork);
            var result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                var errorMessages = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                throw new ValidationException(ResponseCode.CREATED_UNSUCC, errorMessages);
            }

            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = request.Password,
                Email = request.Email,
                RoleId = request.RoleId,
                SchoolId = request.SchoolId,
                GradeId = request.GradeId
            };

            var res = await _unitOfWork.Users.AddAsync(newUser);

            await _unitOfWork.CompleteAsync();

            return new Response<GetUserResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
