using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Cities.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Cities.Queries.GetDistrictsByCityId
{
    public class GetDistrictsByCityIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetDistrictsByCityIdQuery, Response<GetCityDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetCityDetailResponse>> Handle(GetDistrictsByCityIdQuery request, CancellationToken cancellationToken)
        {
            var cityQuery = await _unitOfWork.Cities.GetAsync(
                expression: city => city.CityId == request.CityId,
                includeFunc: city => city.Include(c => c.Districts));

            var city = cityQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.CITY_NOT_FOUND);

            return new Response<GetCityDetailResponse>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<GetCityDetailResponse>(city),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
