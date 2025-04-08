using MediatR;
using TeacherAITools.Application.SchoolSupplies.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.SchoolSupplies.Queries.GetSchoolSupplies
{
    public record GetSchoolSuppliesQuery() : IRequest<Response<List<GetSchoolSupplyResponse>>>;
}
