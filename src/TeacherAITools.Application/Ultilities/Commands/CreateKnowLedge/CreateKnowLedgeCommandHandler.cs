using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Ultilities.Commands.CreateKnowLedge
{
    public class CreateKnowLedgeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateKnowLedgeCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(CreateKnowLedgeCommand request, CancellationToken cancellationToken)
        {
            var newEntity = new KnowLedge
            {
                Goal = request.Goal,
                TeacherActivities = request.TeacherActivities,
                StudentActivities = request.StudentActivities,
                LessonId = request.LessonId,
                Duration = "12-15",
                PromptId = null
            };

            var result = await _unitOfWork.KnowLedeges.AddAsync(newEntity);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
