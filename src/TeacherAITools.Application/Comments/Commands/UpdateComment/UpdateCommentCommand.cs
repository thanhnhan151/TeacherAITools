using MediatR;
using TeacherAITools.Application.Comments.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Comments.Commands.UpdateComment
{
    public record UpdateCommentCommand(
        int BlogId,
        int CommentId,
        CreateUpdateCommentRequest Comment) : IRequest<Response<GetCommentResponse>>;
}
