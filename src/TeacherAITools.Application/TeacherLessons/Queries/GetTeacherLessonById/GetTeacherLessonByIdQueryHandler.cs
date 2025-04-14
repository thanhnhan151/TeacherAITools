using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessonById
{
    public class GetTeacherLessonByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetTeacherLessonByIdQuery, Response<GetDetailTeacherLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetDetailTeacherLessonResponse>> Handle(GetTeacherLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var teacherLessonQuery = await _unitOfWork.TeacherLessons.GetAsync(user => user.LessonPlanId == request.Id);

            var teacherLesson = teacherLessonQuery
                .Include(u => u.Prompt)
                        .ThenInclude(w => w.Lesson)
                                    .ThenInclude(w => w.Module)
                .Include(u => u.User)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            return new Response<GetDetailTeacherLessonResponse>(code: (int)ResponseCode.SUCCESS, data: _mapper.Map<GetDetailTeacherLessonResponse>(teacherLesson), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
