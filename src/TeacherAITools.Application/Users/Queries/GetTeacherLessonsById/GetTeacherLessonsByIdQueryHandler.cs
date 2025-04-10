using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetTeacherLessonsById
{
    public class GetTeacherLessonsByIdQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<GetTeacherLessonsByIdQuery, Response<List<GetUserLessonsResponse>>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<List<GetUserLessonsResponse>>> Handle(GetTeacherLessonsByIdQuery request, CancellationToken cancellationToken)
        {
            var teacherLessons = await _unitOfWork.TeacherLessons.GetAllAsync(
                expression: t => t.UserId == request.Id,
                includeFunc: t => t.Include(t => t.Prompt).ThenInclude(t => t.Lesson));

            return new Response<List<GetUserLessonsResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetUserLessonsResponse>>(teacherLessons),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
