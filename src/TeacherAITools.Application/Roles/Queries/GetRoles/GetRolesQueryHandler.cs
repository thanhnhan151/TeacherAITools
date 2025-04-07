using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Roles.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Roles.Queries.GetRoles
{
    public class GetRolesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetRolesQuery, Response<List<GetRoleResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetRoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<GetRoleResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetRoleResponse>>(await _unitOfWork.Roles.GetAllAsync(expression: r => r.RoleId != 1)),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
