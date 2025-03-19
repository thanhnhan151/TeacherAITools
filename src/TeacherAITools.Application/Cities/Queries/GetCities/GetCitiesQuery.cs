using MediatR;
using TeacherAITools.Application.Cities.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Cities.Queries.GetCities
{
    public record GetCitiesQuery() : IRequest<Response<List<GetCityResponse>>>;
}
