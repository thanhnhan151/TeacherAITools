using MediatR;
using TeacherAITools.Application.Roles.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Roles.Queries.GetRoles
{
    public record GetRolesQuery() : IRequest<Response<List<GetRoleResponse>>>;
}
