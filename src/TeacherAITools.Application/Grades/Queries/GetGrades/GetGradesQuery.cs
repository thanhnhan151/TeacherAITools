using MediatR;
using TeacherAITools.Application.Grades.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Grades.Queries.GetGrades
{
    public record GetGradesQuery() : IRequest<Response<List<GetGradeResponse>>>;
}
