using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Commands.CreatePrompt
{
    public class CreatePromptCommandHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService) : IRequestHandler<CreatePromptCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<string>> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
        {
            string userId = _currentUserService.CurrentPrincipal ?? throw new ApiException(ResponseCode.FAILED_AUTHENTICATION);

            if (await _unitOfWork.Prompts.IsLessonIdPresentAsync(request.LessonId)) throw new ApiException(ResponseCode.ALREADY_EXISTED_LESSON);

            var newPrompt = new Prompt
            {
                Description = request.Description,
                LessonId = request.LessonId,
                UserId = Int32.Parse(userId)
            };

            var result = await _unitOfWork.Prompts.AddAsync(newPrompt);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
