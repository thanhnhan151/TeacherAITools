using MediatR;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Queries.GetModules
{
    public class GetModulesQuery : PaginationRequest<Module>, IRequest<PaginationResponse<GetModuleResponse>>
    {
        public override Expression<Func<Module, bool>> GetExpressions()
        {
            Expression<Func<Module, bool>> expression = m => m.IsActive;
            return expression;
        }
    }
}