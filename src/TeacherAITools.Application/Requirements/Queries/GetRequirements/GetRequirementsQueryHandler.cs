using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Requirements.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Requirements.Queries.GetRequirements
{
    public class GetRequirementsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetRequirementsQuery, Response<List<GetRequirementResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetRequirementResponse>>> Handle(GetRequirementsQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<GetRequirementResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetRequirementResponse>>(await _unitOfWork.Requirements.GetAllAsync()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
