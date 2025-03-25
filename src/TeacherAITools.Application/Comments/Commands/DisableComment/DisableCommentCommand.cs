using MediatR;
using TeacherAITools.Application.Comments.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Comments.Commands.DisableComment
{
    public record DisableCommentCommand(
        int BlogId,
        int CommentId) : IRequest<Response<GetCommentResponse>>;
}
