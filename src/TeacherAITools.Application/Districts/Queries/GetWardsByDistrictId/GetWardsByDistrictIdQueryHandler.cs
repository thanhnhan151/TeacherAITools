using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Districts.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Districts.Queries.GetWardsByDistrictId
{
    public class GetWardsByDistrictIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetWardsByDistrictIdQuery, Response<GetDistrictDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;


        public async Task<Response<GetDistrictDetailResponse>> Handle(GetWardsByDistrictIdQuery request, CancellationToken cancellationToken)
        {
            var districtQuery = await _unitOfWork.Districts.GetAsync(
                expression: district => district.DistrictId == request.DistrictId,
                includeFunc: district => district.Include(c => c.Wards));

            var district = districtQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.DISTRICT_NOT_FOUND);

            return new Response<GetDistrictDetailResponse>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<GetDistrictDetailResponse>(district),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
