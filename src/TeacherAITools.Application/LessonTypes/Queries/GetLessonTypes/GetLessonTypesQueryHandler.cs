using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.LessonTypes.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.LessonTypes.Queries.GetLessonTypes
{
    public class GetLessonTypesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetLessonTypesQuery, Response<List<GetLessonTypeResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetLessonTypeResponse>>> Handle(GetLessonTypesQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<GetLessonTypeResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetLessonTypeResponse>>(await _unitOfWork.LessonTypes.GetAllAsync()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
