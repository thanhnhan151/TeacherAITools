using MediatR;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Queries.GetSchools
{
    public class GetSchoolsQuery : PaginationRequest<School>, IRequest<PaginationResponse<GetSchoolResponse>>
    {
        public override Expression<Func<School, bool>> GetExpressions()
        {
            Expression<Func<School, bool>> expression = _ => true;
            return expression;
        }
    }
}
