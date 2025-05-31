using MediatR;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Queries.GetLessonsByModuleId
{
    public record GetLessonsByModuleIdQuery(int ModuleId, int? RoleId) : IRequest<Response<GetModuleDetailResponse>>;
}
