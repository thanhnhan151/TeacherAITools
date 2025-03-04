using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<GetUsersQuery, Response<List<GetUserResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<List<GetUserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = new List<GetUserResponse>();

            var userQuery = await _unitOfWork.Users.GetAllAsync(includeFunc: user => user.Include(u => u.Role));

            var users = userQuery.ToList();

            foreach (var user in users)
            {
                result.Add(new GetUserResponse
                {
                    Id = user.UserId,
                    Fullname = user.Fullname,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender.GetDescription(),
                    Role = user.Role.RoleName
                });
            }

            return new Response<List<GetUserResponse>>(code: (int)ResponseCode.SUCCESS, data: result, message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
