using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Grades.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Grades.Queries.GetGrades
{
    public class GetGradesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetGradesQuery, Response<List<GetGradeResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetGradeResponse>>> Handle(GetGradesQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<GetGradeResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetGradeResponse>>(await _unitOfWork.Grades.GetAllAsync()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
