using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.LessonHistories.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.LessonHistories.Queries.GetLessonHistories
{
    public class GetLessonHistoriesQuery : PaginationRequest<LessonHistory>, IRequest<PaginationResponse<GetLessonHistoryResponse>>
    {
        public override Expression<Func<LessonHistory, bool>> GetExpressions()
        {
            Expression<Func<LessonHistory, bool>> expression = _ => true;

            return expression;
        }
    }
}