using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Ultilities.Commands.CreateApply
{
    public class CreateApplyCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateApplyCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(CreateApplyCommand request, CancellationToken cancellationToken)
        {
            var newEntity = new Apply
            {
                Goal = request.Goal,
                TeacherActivities = request.TeacherActivities,
                StudentActivities = request.StudentActivities,
                LessonId = request.LessonId,
                Duration = "3-5",
                PromptId = null
            };

            var result = await _unitOfWork.Applies.AddAsync(newEntity);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
