using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery, Response<GetUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userQuery = await _unitOfWork.Users.GetAsync(user => user.UserId == request.UserId);

            var user = userQuery.Include(r => r.Role).FirstOrDefault() ?? throw new ApiException(ResponseCode.USER_NOT_FOUND);

            var response = new GetUserResponse
            {
                Id = user.UserId,
                Fullname = user.Fullname,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender.GetDescription(),
                Role = user.Role.RoleName
            };

            return new Response<GetUserResponse>(code: (int)ResponseCode.SUCCESS, data: response, message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
