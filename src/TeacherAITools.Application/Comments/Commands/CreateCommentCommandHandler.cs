using MediatR;
using TeacherAITools.Application.Comments.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Comments.Commands
{
    public class CreateCommentCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<CreateCommentCommand, Response<GetCommentResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetCommentResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var newComment = new Comment
            {
                CommentBody = request.CommentBody,
                TimeStamp = request.TimeStamp,
                UserId = request.UserId,
                BlogId = request.BlogId
            };

            var result = await _unitOfWork.Comments.AddAsync(newComment);

            await _unitOfWork.CompleteAsync();

            return new Response<GetCommentResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
