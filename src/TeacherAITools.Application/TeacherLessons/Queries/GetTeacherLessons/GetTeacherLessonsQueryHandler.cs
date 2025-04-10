using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessons
{
    public class GetTeacherLessonsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetTeacherLessonsQuery, Response<PaginatedList<GetTeacherLessonResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<PaginatedList<GetTeacherLessonResponse>>> Handle(GetTeacherLessonsQuery request, CancellationToken cancellationToken)
        {
            return new Response<PaginatedList<GetTeacherLessonResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<PaginatedList<GetTeacherLessonResponse>>(await _unitOfWork.TeacherLessons.PaginatedListAsync(
                    request.SearchTerm,
                    request.SortColumn,
                    request.SortOrder,
                    request.ModuleId,
                    request.LessonId,
                    request.UserId,
                    request.Status,
                    request.Page,
                    request.PageSize
                )),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
