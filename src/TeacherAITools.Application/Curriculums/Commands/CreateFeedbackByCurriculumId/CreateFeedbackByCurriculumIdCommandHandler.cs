using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.CreateFeedbackByCurriculumId
{
    public class CreateFeedbackByCurriculumIdCommandHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateFeedbackByCurriculumIdCommand, Response<GetCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        public async Task<Response<GetCurriculumResponse>> Handle(CreateFeedbackByCurriculumIdCommand request, CancellationToken cancellationToken)
        {
            string userId = _currentUserService.CurrentPrincipal ?? throw new ApiException(ResponseCode.FAILED_AUTHENTICATION);

            var newFeedback = new CurriculumFeedback
            {
                Body = request.Feedback.Body,
                TimeStamp = _dateTimeProvider.UtcNow,
                UserId = Int32.Parse(userId),
                CurriculumId = request.CurriculumId
            };

            var result = await _unitOfWork.CurriculumFeedbacks.AddAsync(newFeedback);

            await _unitOfWork.CompleteAsync();

            return new Response<GetCurriculumResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
