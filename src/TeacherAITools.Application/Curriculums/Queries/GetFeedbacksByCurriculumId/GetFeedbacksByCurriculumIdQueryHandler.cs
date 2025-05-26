using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Queries.GetFeedbacksByCurriculumId
{
    public class GetFeedbacksByCurriculumIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetFeedbacksByCurriculumIdQuery, Response<List<GetCurriculumFeedbackResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetCurriculumFeedbackResponse>>> Handle(GetFeedbacksByCurriculumIdQuery request, CancellationToken cancellationToken)
        {
            var curriculumQuery = await _unitOfWork.Curriculums.GetAsync(curriculum => curriculum.CurriculumId == request.CurriculumId);

            var curriculum = curriculumQuery.Include(c => c.CurriculumFeedbacks).ThenInclude(c => c.User).FirstOrDefault() ?? throw new ApiException(ResponseCode.CURRICULUM_NOT_FOUND);

            return new Response<List<GetCurriculumFeedbackResponse>>(code: (int)ResponseCode.SUCCESS, data: _mapper.Map<List<GetCurriculumFeedbackResponse>>(curriculum.CurriculumFeedbacks), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
