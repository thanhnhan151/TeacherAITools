using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Commands.CreatePrompt
{
    public class CreatePromptCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<CreatePromptCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Prompts.IsLessonIdPresentAsync(request.LessonId)) throw new ApiException(ResponseCode.ALREADY_EXISTED_LESSON);

            var newPrompt = new Prompt
            {
                Description = request.Description,
                LessonId = request.LessonId
            };

            var result = await _unitOfWork.Prompts.AddAsync(newPrompt);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
