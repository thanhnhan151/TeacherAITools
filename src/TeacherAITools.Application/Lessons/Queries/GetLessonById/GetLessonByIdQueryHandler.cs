using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessonById
{
    public class GetLessonByIdQueryHandler : IRequestHandler<GetLessonByIdQuery, Response<GetLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLessonByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetLessonResponse>> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(lesson => lesson.LessonId == request.id);

            var lesson = lessonQuery
                .Include(l => l.LessonType)
                .Include(l => l.Requirement)
                .Include(l => l.TeachingTool)
                .Include(l => l.Note)
                .Include(l => l.User)
                .Include(l => l.Week)
                .Include(l => l.Module)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            var response = new GetLessonResponse
            {
                LessonId = lesson.LessonId,
                Name = lesson.Name,
                Description = lesson.Description,
                TotalPeriods = lesson.TotalPeriods,
                IsApproved = lesson.IsApproved,
                IsPublic = lesson.IsPublic,
                LessonType = lesson.LessonType.LessonTypeName,
                Requirement = lesson.Requirement.Description,
                TeachingTool = lesson.TeachingTool.Description,
                Note = lesson.Note.Description,
                User = lesson.User.Username,
                Week = lesson.Week.WeekNumber,
                Module = lesson.Module.Name
            };

            return new Response<GetLessonResponse>(code: (int)ResponseCode.SUCCESS, data: response, message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
