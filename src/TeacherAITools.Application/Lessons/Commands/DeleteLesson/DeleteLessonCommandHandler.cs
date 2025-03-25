using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetLessonResponse>> Handle(int request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(expression: m => m.LessonId == request, disableTracking: true);

            var lesson = lessonQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            await _unitOfWork.Lessons.UpdateAsync(lesson);
            await _unitOfWork.CompleteAsync();

            return new Response<GetLessonResponse>(code: (int)ResponseCode.DELETED_SUCCESS, message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}
