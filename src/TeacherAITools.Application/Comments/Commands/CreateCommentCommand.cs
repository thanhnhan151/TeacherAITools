using MediatR;
using TeacherAITools.Application.Comments.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Comments.Commands
{
    public record CreateCommentCommand(
    string CommentBody,
    DateTime TimeStamp,
    int UserId,
    int BlogId) : IRequest<Response<GetCommentResponse>>;
}
