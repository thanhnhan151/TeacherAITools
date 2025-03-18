using System.Linq.Expressions;
using MediatR;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Queries.GetCurriculums
{
    public class GetCurriculumsQuery : PaginationRequest<Curriculum>, IRequest<PaginationResponse<GetCurriculumResponse>>
    {
        public override Expression<Func<Curriculum, bool>> GetExpressions()
        {
            Expression<Func<Curriculum, bool>> expression = _ => true;
            return expression;
        }
    }
}