
using MediatR;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Commands.DeleteModule
{
    public record DeleteModuleCommand(int id) : IRequest<Response<GetModuleResponse>>;
}