using MediatR;
using TeacherAITools.Application.Comments.Commands.CreateComment;
using TeacherAITools.Application.Comments.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Comments.Commands.CreateCommand
{
    public class CreateCommentCommandHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateCommentCommand, Response<GetCommentResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        public async Task<Response<GetCommentResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            string userId = _currentUserService.CurrentPrincipal ?? throw new ApiException(ResponseCode.FAILED_AUTHENTICATION);

            var newComment = new Comment
            {
                CommentBody = request.Comment.Body,
                TimeStamp = _dateTimeProvider.UtcNow,
                UserId = Int32.Parse(userId),
                BlogId = request.Id
            };

            var result = await _unitOfWork.Comments.AddAsync(newComment);

            await _unitOfWork.CompleteAsync();

            return new Response<GetCommentResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
