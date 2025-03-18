using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Queries.GetModuleById
{
    public class GetModuleByIdQueryHandler : IRequestHandler<GetModuleByIdQuery, Response<GetModuleResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetModuleByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetModuleResponse>> Handle(GetModuleByIdQuery request, CancellationToken cancellationToken)
        {
            var moduleQuery = await _unitOfWork.Modules.GetAsync(module => module.ModuleId == request.id);

            var module = moduleQuery.Include(c => c.Curriculum).FirstOrDefault() ?? throw new ApiException(ResponseCode.MODULE_NOT_FOUND);

            var response = new GetModuleResponse
            {
                ModuleId = module.ModuleId,
                Name = module.Name,
                Desciption = module.Desciption,
                Semester = module.Semester,
                TotalPeriods = module.TotalPeriods,
                Curriculum = module.Curriculum.Name
            };

            return new Response<GetModuleResponse>(code: (int)ResponseCode.SUCCESS, data: response, message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}