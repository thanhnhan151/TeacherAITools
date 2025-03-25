using MediatR;
using TeacherAITools.Application.Comments.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Comments.Commands.CreateComment
{
    public record CreateCommentCommand(
    int Id,
    CreateUpdateCommentRequest Comment) : IRequest<Response<GetCommentResponse>>;
}
