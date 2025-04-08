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
    public class GetCurriculumByIdQueryHandler : IRequestHandler<GetCurriculumByIdQuery, Response<GetCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCurriculumByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetCurriculumResponse>> Handle(GetCurriculumByIdQuery request, CancellationToken cancellationToken)
        {
            var curriculumQuery = await _unitOfWork.Curriculums.GetAsync(curriculum => curriculum.CurriculumId == request.id);

            var curriculum = curriculumQuery/*.Include(c => c.Grade)*/.Include(c => c.SchoolYear).FirstOrDefault() ?? throw new ApiException(ResponseCode.CURRICULUM_NOT_FOUND);

            var response = new GetCurriculumResponse
            {
                CurriculumId = curriculum.CurriculumId,
                Name = curriculum.Name,
                Description = curriculum.Description,
                TotalPeriods = curriculum.TotalPeriods,
                Year = curriculum.SchoolYear.Year
            };

            return new Response<GetCurriculumResponse>(code: (int)ResponseCode.SUCCESS, data: response, message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}