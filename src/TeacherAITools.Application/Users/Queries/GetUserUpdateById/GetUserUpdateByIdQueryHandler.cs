using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetUserUpdateById
{
    public class GetUserUpdateByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetUserUpdateByIdQuery, Response<GetUserUpdateResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetUserUpdateResponse>> Handle(GetUserUpdateByIdQuery request, CancellationToken cancellationToken)
        {
            var userQuery = await _unitOfWork.Users.GetAsync(user => user.UserId == request.Id);

            var user = userQuery
                .Include(r => r.Role)
                .Include(u => u.Manager)
                .Include(u => u.School)
                .Include(u => u.Grade)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.USER_NOT_FOUND);

            return new Response<GetUserUpdateResponse>(code: (int)ResponseCode.SUCCESS, data: _mapper.Map<GetUserUpdateResponse>(user), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
