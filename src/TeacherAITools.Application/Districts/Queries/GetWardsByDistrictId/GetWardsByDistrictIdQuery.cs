using MediatR;
using TeacherAITools.Application.Districts.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Districts.Queries.GetWardsByDistrictId
{
    public record GetWardsByDistrictIdQuery(int DistrictId) : IRequest<Response<GetDistrictDetailResponse>>;
}
