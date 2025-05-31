using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteLessonCommand, Response<GetLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetLessonResponse>> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(
                expression: m => m.LessonId == request.Id
                , includeFunc: m => m.Include(m => m.Module).ThenInclude(m => m.Curriculum)
                , disableTracking: true);

            var lesson = lessonQuery
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            if (lesson.IsActive)
            {
                lesson.IsActive = false;
                lesson.Module.TotalPeriods -= lesson.TotalPeriods;
                lesson.Module.Curriculum.TotalPeriods -= lesson.TotalPeriods;
            }
            else
            {
                lesson.IsActive = true;
                lesson.Module.TotalPeriods += lesson.TotalPeriods;
                lesson.Module.Curriculum.TotalPeriods += lesson.TotalPeriods;
            }

            await _unitOfWork.Lessons.UpdateAsync(lesson);

            await _unitOfWork.Modules.UpdateAsync(lesson.Module);

            await _unitOfWork.Curriculums.UpdateAsync(lesson.Module.Curriculum);

            await _unitOfWork.CompleteAsync();

            return new Response<GetLessonResponse>(code: (int)ResponseCode.DELETED_SUCCESS, message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}
