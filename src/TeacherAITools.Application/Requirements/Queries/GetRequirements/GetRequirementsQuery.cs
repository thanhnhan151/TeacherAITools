using MediatR;
using TeacherAITools.Application.Requirements.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Requirements.Queries.GetRequirements
{
    public record GetRequirementsQuery() : IRequest<Response<List<GetRequirementResponse>>>;
}
