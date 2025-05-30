using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Grades.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Grades.Queries.GetModulesByGradeId
{
    public class GetModulesByGradeIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetModulesByGradeIdQuery, Response<GetGradeDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetGradeDetailResponse>> Handle(GetModulesByGradeIdQuery request, CancellationToken cancellationToken)
        {
            var gradeQuery = await _unitOfWork.Grades.GetAsync(g => g.GradeId == request.GradeId);

            var grade = gradeQuery.Include(g => g.Modules.Where(m => m.IsActive).OrderBy(m => m.ModuleId)).FirstOrDefault() ?? throw new ApiException(ResponseCode.ID_GRADE_DONT_EXIST);

            return new Response<GetGradeDetailResponse>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<GetGradeDetailResponse>(grade),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
