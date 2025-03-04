using MediatR;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetUsers
{
    //public record GetUsersQuery : IRequest<Response<List<GetUserResponse>>>;

    public class GetUsersQuery : PaginationRequest<User>, IRequest<PaginationResponse<GetUserResponse>>
    {
        public override Expression<Func<User, bool>> GetExpressions()
        {
            Expression<Func<User, bool>> expression = _ => true;
            return expression;
        }
    }
}
