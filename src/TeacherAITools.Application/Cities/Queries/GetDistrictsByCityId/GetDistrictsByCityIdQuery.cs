using MediatR;
using TeacherAITools.Application.Cities.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Cities.Queries.GetDistrictsByCityId
{
    public record GetDistrictsByCityIdQuery(int CityId) : IRequest<Response<GetCityDetailResponse>>;
}
