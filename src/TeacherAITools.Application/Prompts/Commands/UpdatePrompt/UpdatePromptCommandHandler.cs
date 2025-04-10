using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Commands.UpdatePrompt
{
    public class UpdatePromptCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<UpdatePromptCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(UpdatePromptCommand request, CancellationToken cancellationToken)
        {
            var promptQuery = await _unitOfWork.Prompts.GetAsync(p => p.PromptId == request.Id);

            var result = promptQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.PROMPT_NOT_FOUND);

            result.Description = request.Description;

            await _unitOfWork.Prompts.UpdateAsync(result);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.UPDATED_SUCCESS,
                message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
