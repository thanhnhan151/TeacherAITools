using AutoMapper;
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
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetUserByIdQuery, Response<GetUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userQuery = await _unitOfWork.Users.GetAsync(user => user.UserId == request.UserId);

            var user = userQuery
                .Include(r => r.Role)
                .Include(u => u.Manager)
                .Include(u => u.School)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.USER_NOT_FOUND);

            return new Response<GetUserResponse>(code: (int)ResponseCode.SUCCESS, data: _mapper.Map<GetUserResponse>(user), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
