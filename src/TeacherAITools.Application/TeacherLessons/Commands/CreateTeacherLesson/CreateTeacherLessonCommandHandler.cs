using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.CreateTeacherLesson
{
    public class CreateTeacherLessonCommandHandler(
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateTeacherLessonCommand, Response<GetTeacherLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        public async Task<Response<GetTeacherLessonResponse>> Handle(CreateTeacherLessonCommand request, CancellationToken cancellationToken)
        {
            //if (await _unitOfWork.TeacherLessons.IsBelongedToTeacherAsync(request.UserId, request.PromptId)) throw new ApiException(ResponseCode.ALREADY_GENERATED_LESSON);

            TeacherLesson teacherLesson = new()
            {
                StartUp = request.StartUp,
                Knowledge = request.KnowLedge,
                Practice = request.Practice,
                Apply = request.Apply,
                Goal = request.Goal,
                SchoolSupply = request.SchoolSupply,
                UserId = request.UserId,
                PromptId = null,
                CreatedAt = _dateTimeProvider.UtcNow
            };

            await _unitOfWork.TeacherLessons.AddAsync(teacherLesson);

            await _unitOfWork.CompleteAsync();

            return new Response<GetTeacherLessonResponse>(code: (int)ResponseCode.CREATED_SUCCESS,
                message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
