using MediatR;
using TeacherAITools.Application.Comments.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Comments.Commands.DisableComment
{
    public class DisableCommentCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<DisableCommentCommand, Response<GetCommentResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetCommentResponse>> Handle(DisableCommentCommand request, CancellationToken cancellationToken)
        {
            var blogQuery = await _unitOfWork.Blogs.GetAsync(expression: u => u.BlogId == request.BlogId, disableTracking: true);

            var blog = blogQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.BLOG_NOT_FOUND);

            var commentQuery = await _unitOfWork.Comments.GetAsync(expression: u => u.CommentId == request.CommentId, disableTracking: true);

            var comment = commentQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.COMMENT_NOT_FOUND);

            if (comment.Status)
            {
                comment.Status = false;
            }
            else
            {
                comment.Status = true;
            }

            await _unitOfWork.Comments.UpdateAsync(comment);

            await _unitOfWork.CompleteAsync();

            return new Response<GetCommentResponse>(code: (int)ResponseCode.DISABLED_SUCCESS, message: ResponseCode.DISABLED_SUCCESS.GetDescription());
        }
    }
}
