using MediatR;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Commands.CreateModule{
    public record CreateModuleCommand(
        CreateModuleRequest createModuleRequest) : IRequest<Response<GetModuleResponse>>;
}