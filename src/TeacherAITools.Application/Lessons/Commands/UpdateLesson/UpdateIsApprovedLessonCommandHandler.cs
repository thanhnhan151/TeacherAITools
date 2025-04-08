using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateIsApprovedLesson
{
    public class UpdateIsApprovedLessonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateIsApprovedLessonCommand, Response<GetLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetLessonResponse>> Handle(UpdateIsApprovedLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(expression: m => m.LessonId == request.Id, disableTracking: true);

            var lesson = lessonQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            //lesson.IsApproved = request.updateIsApprovedRequest.IsApproved;

            //if (lesson.IsApproved == false)
            //{
            //    lesson.DisapprovedReason = request.updateIsApprovedRequest.DisapprovedReason;
            //}

            await _unitOfWork.Lessons.UpdateAsync(lesson);
            await _unitOfWork.CompleteAsync();

            return new Response<GetLessonResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}