using MediatR;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Queries.GetModuleById
{
    public record GetModuleByIdQuery(int id) : IRequest<Response<GetModuleResponse>>;
}