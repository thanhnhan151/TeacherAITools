using MediatR;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.UpdateUser
{
    public record UpdateModuleCommand(
        int Id,
        UpdateModuleRequest updateModuleRequest) : IRequest<Response<GetModuleResponse>>;
}
