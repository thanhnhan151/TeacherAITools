using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetUsers
{
    //public record GetUsersQuery : IRequest<Response<List<GetUserResponse>>>;

    //public class GetUsersQuery : PaginationRequest<User>, IRequest<PaginationResponse<GetUserResponse>>
    //{
    //    public string? Query { get; set; }

    //    public int? RoleId { get; set; }

    //    public override Expression<Func<User, bool>> GetExpressions()
    //    {
    //        Expression<Func<User, bool>> expression = _ => true;
    //        expression = user =>
    //            (string.IsNullOrEmpty(Query) || user.Fullname.Contains(Query)) &&
    //            (RoleId == null || user.RoleId == RoleId);

    //        return expression;
    //    }
    //}

    public record GetUsersQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int? IsActive,
        int? RoleId,
        int? GradeId,
        int? SchoolId,
        int Page,
        int PageSize) : IRequest<Response<PaginatedList<GetUserResponse>>>;
}
