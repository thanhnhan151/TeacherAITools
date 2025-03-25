using MediatR;
using TeacherAITools.Application.Categories.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery() : IRequest<Response<List<GetCategoryResponse>>>;
}
