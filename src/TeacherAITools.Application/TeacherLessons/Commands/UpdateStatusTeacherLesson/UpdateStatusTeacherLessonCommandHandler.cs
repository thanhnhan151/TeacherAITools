using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Commands.UpdateStatusTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.UpdateStatusTeacherLesson
{
    public class UpdateStatusTeacherLessonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateStatusTeacherLessonCommand, Response<GetDetailTeacherLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetDetailTeacherLessonResponse>> Handle(UpdateStatusTeacherLessonCommand request, CancellationToken cancellationToken)
        {
            var query = await _unitOfWork.TeacherLessons.GetAsync(expression: m => m.LessonPlanId == request.Id, disableTracking: true);

            var teacherLesson = query.FirstOrDefault() ?? throw new ApiException(ResponseCode.TEACHER_LESSON_DONT_EXIST);

            teacherLesson.Status = request.updateStatusTeacherLessonRequest.Status;

            switch (request.updateStatusTeacherLessonRequest.Status)
            {
                case Domain.Common.LessonStatus.Rejected:
                    if (string.IsNullOrEmpty(request.updateStatusTeacherLessonRequest.DisapprovedReason))
                    {
                        throw new ApiException(ResponseCode.MUST_HAVE_DISAPPROVED_REASON);
                    }

                    teacherLesson.DisapprovedReason = request.updateStatusTeacherLessonRequest.DisapprovedReason;
                    break;
                case Domain.Common.LessonStatus.Draft:
                    teacherLesson.DisapprovedReason = string.Empty;
                    break;
                case Domain.Common.LessonStatus.Pending:
                case Domain.Common.LessonStatus.Approved:
                    //case Domain.Common.LessonStatus.Cancelled:
                    break;
            }

            await _unitOfWork.TeacherLessons.UpdateAsync(teacherLesson);
            await _unitOfWork.CompleteAsync();

            return new Response<GetDetailTeacherLessonResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
