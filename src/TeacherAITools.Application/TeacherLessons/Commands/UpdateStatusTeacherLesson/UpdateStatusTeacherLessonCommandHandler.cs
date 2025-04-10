using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateStatusTeacherLesson
{
    public class UpdateStatusTeacherLessonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateStatusTeacherLessonCommand, Response<GetDetailTeacherLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetDetailTeacherLessonResponse>> Handle(UpdateStatusTeacherLessonCommand request, CancellationToken cancellationToken)
        {
            var query = await _unitOfWork.TeacherLessons.GetAsync(expression: m => m.TeacherLessonId == request.Id, disableTracking: true);

            var teacherLesson = query.FirstOrDefault() ?? throw new ApiException(ResponseCode.TEACHER_LESSON_DONT_EXIST);

            teacherLesson.Status = request.updateStatusTeacherLessonRequest.Status;

            if(request.updateStatusTeacherLessonRequest.Status == Domain.Common.LessonStatus.Rejected 
            && string.IsNullOrEmpty(request.updateStatusTeacherLessonRequest.DisapprovedReason) ){
                teacherLesson.DisapprovedReason = request.updateStatusTeacherLessonRequest.DisapprovedReason;
            }

            await _unitOfWork.TeacherLessons.UpdateAsync(teacherLesson);
            await _unitOfWork.CompleteAsync();

            return new Response<GetDetailTeacherLessonResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
