using MediatR;
using TeacherAITools.Application.Weeks.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Weeks.Queries.GetWeeks
{
    public record GetWeeksQuery() : IRequest<Response<List<GetWeekResponse>>>;
}
