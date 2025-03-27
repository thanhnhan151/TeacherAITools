using MediatR;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Queries.GetSchools
{
    //public class GetSchoolsQuery : PaginationRequest<School>, IRequest<PaginationResponse<GetSchoolResponse>>
    //{
    //    public override Expression<Func<School, bool>> GetExpressions()
    //    {
    //        Expression<Func<School, bool>> expression = _ => true;
    //        return expression;
    //    }
    //}

    public record GetSchoolsQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        bool Status,
        int Page,
        int PageSize) : IRequest<Response<PaginatedList<GetSchoolResponse>>>;
}
