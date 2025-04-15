using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Queries.GetCurriculumById
{
    public class GetCurriculumByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetCurriculumByIdQuery, Response<GetDetailCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetDetailCurriculumResponse>> Handle(GetCurriculumByIdQuery request, CancellationToken cancellationToken)
        {
            var curriculumQuery = await _unitOfWork.Curriculums.GetAsync(curriculum => curriculum.CurriculumId == request.Id);

            var curriculum = curriculumQuery
                .Include(c => c.SchoolYear)
                .Include(c => c.CurriculumActivities)
                .Include(c => c.CurriculumDetails)
                        .ThenInclude(c => c.CurriculumSubSection)
                                    .ThenInclude(c => c.CurriculumSection)
                                                .ThenInclude(c => c.CurriculumTopic)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.CURRICULUM_NOT_FOUND);

            return new Response<GetDetailCurriculumResponse>(code: (int)ResponseCode.SUCCESS, data: _mapper.Map<GetDetailCurriculumResponse>(curriculum), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}