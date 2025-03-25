using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, Response<GetLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetLessonResponse>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(expression: m => m.LessonId == request.Id, disableTracking: true);

            var lesson = lessonQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            lesson.Name = request.updateLessonRequest.Name;
            lesson.Description = request.updateLessonRequest.Description;
            lesson.TotalPeriods = request.updateLessonRequest.TotalPeriods;
            lesson.LessonTypeId = request.updateLessonRequest.LessonTypeId;
            lesson.RequirementId = request.updateLessonRequest.RequirementId;
            lesson.TeachingToolId = request.updateLessonRequest.TeachingToolId;
            lesson.NoteId = request.updateLessonRequest.NoteId;
            lesson.UserId = request.updateLessonRequest.UserId;
            lesson.WeekId = request.updateLessonRequest.WeekId;
            lesson.ModuleId = request.updateLessonRequest.ModuleId;

            await _unitOfWork.Lessons.UpdateAsync(lesson);
            await _unitOfWork.CompleteAsync();
            
            return new Response<GetLessonResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
