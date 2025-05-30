using MediatR;
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
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(expression: m => m.LessonId == request.Id, disableTracking: true);

            var lesson = lessonQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            if (lesson.IsActive)
            {
                lesson.IsActive = false;
            }
            else
            {
                lesson.IsActive = true;
            }

            await _unitOfWork.Lessons.UpdateAsync(lesson);
            await _unitOfWork.CompleteAsync();

            return new Response<GetLessonResponse>(code: (int)ResponseCode.DELETED_SUCCESS, message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}
