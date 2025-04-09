using MediatR;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessons
{
    public class GetLessonsQuery : PaginationRequest<Lesson>, IRequest<PaginationResponse<GetLessonResponse>>
    {
        public override Expression<Func<Lesson, bool>> GetExpressions()
        {
            Expression<Func<Lesson, bool>> expression = _ => true;

            return expression;
        }
    }
}