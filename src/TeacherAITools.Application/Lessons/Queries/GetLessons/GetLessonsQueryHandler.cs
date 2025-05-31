using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessons
{
    public class GetLessonsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetLessonsQuery, Response<PaginatedList<GetLessonResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<PaginatedList<GetLessonResponse>>> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
        {
            return new Response<PaginatedList<GetLessonResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<PaginatedList<GetLessonResponse>>(await _unitOfWork.Lessons.PaginatedListAsync(
                    request.SearchTerm,
                    request.SortColumn,
                    request.SortOrder,
                    request.LessonTypeId,
                    request.ModuleId,
                    request.IsActive,
                    request.Page,
                    request.PageSize
                )),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
