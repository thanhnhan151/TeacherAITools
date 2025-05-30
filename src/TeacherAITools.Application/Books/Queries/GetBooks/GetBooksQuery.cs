using MediatR;
using TeacherAITools.Application.Books.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Books.Queries.GetBooks
{
    public record GetBooksQuery() : IRequest<Response<List<GetBookResponse>>>;
}
