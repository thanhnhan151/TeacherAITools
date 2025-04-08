using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.SchoolSupplies.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.SchoolSupplies.Queries.GetSchoolSupplies
{
    public class GetSchoolSuppliesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetSchoolSuppliesQuery, Response<List<GetSchoolSupplyResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetSchoolSupplyResponse>>> Handle(GetSchoolSuppliesQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<GetSchoolSupplyResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetSchoolSupplyResponse>>(await _unitOfWork.SchoolSupplies.GetAllAsync()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
