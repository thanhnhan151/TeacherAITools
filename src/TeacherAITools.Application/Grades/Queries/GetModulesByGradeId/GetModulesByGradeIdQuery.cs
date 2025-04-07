using MediatR;
using TeacherAITools.Application.Grades.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Grades.Queries.GetModulesByGradeId
{
    public record GetModulesByGradeIdQuery(int GradeId) : IRequest<Response<GetGradeDetailResponse>>;
}
