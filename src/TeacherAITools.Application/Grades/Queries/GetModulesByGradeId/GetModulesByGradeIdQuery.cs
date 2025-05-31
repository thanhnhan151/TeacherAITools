using MediatR;
using TeacherAITools.Application.Grades.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Grades.Queries.GetModulesByGradeId
{
    public record GetModulesByGradeIdQuery(
        int GradeId
        , int? RoleId) : IRequest<Response<GetGradeDetailResponse>>;
}
