using MediatR;
using TeacherAITools.Application.SchoolYears.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.SchoolYears.Queries.GetSchoolYears
{
    public record GetSchoolYearsQuery() : IRequest<Response<List<GetSchoolYearResponse>>>;
}
