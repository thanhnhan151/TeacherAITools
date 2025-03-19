using AutoMapper;
using MediatR;
using TeacherAITools.Application.Cities.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Cities.Queries.GetCities
{
    public class GetCitiesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetCitiesQuery, Response<List<GetCityResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetCityResponse>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cityQuery = await _unitOfWork.Cities.GetAllAsync();

            return new Response<List<GetCityResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetCityResponse>>(cityQuery.ToList()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
