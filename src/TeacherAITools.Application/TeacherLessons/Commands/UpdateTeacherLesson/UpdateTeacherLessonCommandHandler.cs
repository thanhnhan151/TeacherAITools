using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateTeacherLesson
{
    public class UpdateTeacherLessonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateTeacherLessonCommand, Response<GetDetailTeacherLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetDetailTeacherLessonResponse>> Handle(UpdateTeacherLessonCommand request, CancellationToken cancellationToken)
        {
            var query = await _unitOfWork.TeacherLessons.GetAsync(expression: m => m.TeacherLessonId == request.Id, disableTracking: true);

            var teacherLesson = query.FirstOrDefault() ?? throw new ApiException(ResponseCode.TEACHER_LESSON_DONT_EXIST);

            var lessonHistory = new LessonHistory{
                StartUp = teacherLesson.StartUp,
                Knowledge = teacherLesson.Knowledge,
                Goal = teacherLesson.Goal,
                SchoolSupply = teacherLesson.SchoolSupply,
                Practice = teacherLesson.Practice,
                Apply = teacherLesson.Apply,
            };

            teacherLesson.StartUp = request.updateTeacherLessonRequest.StartUp;
            teacherLesson.Knowledge = request.updateTeacherLessonRequest.Knowledge;
            teacherLesson.Goal = request.updateTeacherLessonRequest.Goal;
            teacherLesson.SchoolSupply = request.updateTeacherLessonRequest.SchoolSupply;
            teacherLesson.Practice = request.updateTeacherLessonRequest.Practice;
            teacherLesson.Apply = request.updateTeacherLessonRequest.Apply;

            await _unitOfWork.LessonHistories.AddAsync(lessonHistory);
            await _unitOfWork.TeacherLessons.UpdateAsync(teacherLesson);
            await _unitOfWork.CompleteAsync();

            return new Response<GetDetailTeacherLessonResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
