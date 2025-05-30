using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessonInfoById
{
    public class GetLessonInfoByIdQueryHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<GetLessonInfoByIdQuery, Response<GetLessonInfoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetLessonInfoResponse>> Handle(GetLessonInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(lesson => lesson.LessonId == request.Id);

            var lesson = lessonQuery
                .Include(l => l.LessonType)
                .Include(l => l.Note)
                .Include(l => l.Module)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            var response = new GetLessonInfoResponse
            {
                LessonId = lesson.LessonId,
                Name = lesson.Name,
                Description = lesson.Description,
                TotalPeriods = lesson.TotalPeriods,
                LessonTypeId = lesson.LessonType.LessonTypeId,
                LessonType = lesson.LessonType.LessonTypeName,
                NoteId = lesson.Note.NoteId,
                Note = lesson.Note.Description,
                ModuleId = lesson.Module.ModuleId,
                Module = lesson.Module.Name,
                GradeNumber = lesson.Module.GradeId,
            };

            return new Response<GetLessonInfoResponse>(code: (int)ResponseCode.SUCCESS, data: response, message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
