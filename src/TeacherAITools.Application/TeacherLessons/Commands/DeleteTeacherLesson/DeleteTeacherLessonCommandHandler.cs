using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.DeleteTeacherLesson
{
    public class DeleteTeacherLessonCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteTeacherLessonCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(DeleteTeacherLessonCommand request, CancellationToken cancellationToken)
        {
            var teacherLessonQuery = await _unitOfWork.TeacherLessons.GetAsync(user => user.LessonPlanId == request.Id);

            var teacherLesson = teacherLessonQuery
                .Include(u => u.Prompt)
                .Include(u => u.LessonHistories)
                .Include(u => u.Blogs)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            if (teacherLesson.LessonHistories.Count >= 1)
            {
                await _unitOfWork.LessonHistories.DeleteRangeAsync(teacherLesson.LessonHistories);

                //await _unitOfWork.CompleteAsync();
            }

            if (teacherLesson.Blogs.Count >= 1)
            {
                foreach (var item in teacherLesson.Blogs)
                {
                    item.LessonPlanId = null;
                    item.IsActive = false;
                }

                await _unitOfWork.Blogs.UpdateRangeAsync(teacherLesson.Blogs);

                //await _unitOfWork.CompleteAsync();
            }

            await _unitOfWork.Prompts.DeleteAsync(teacherLesson.Prompt);

            //await _unitOfWork.CompleteAsync();

            await _unitOfWork.TeacherLessons.DeleteAsync(teacherLesson);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.DELETED_SUCCESS,
                message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}
