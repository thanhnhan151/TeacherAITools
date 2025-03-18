using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Application.Users.Commands.UpdateUser;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Commands.UpdateModule
{
    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, Response<GetModuleResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetModuleResponse>> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleQuery = await _unitOfWork.Modules.GetAsync(expression: m => m.ModuleId == request.Id, disableTracking: true);

            var module = moduleQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.MODULE_NOT_FOUND);

            module.Name = request.updateModuleRequest.Name;
            module.Desciption = request.updateModuleRequest.Desciption;
            module.Semester = request.updateModuleRequest.Semester;
            module.TotalPeriods = request.updateModuleRequest.TotalPeriods;
            module.CurriculumId = request.updateModuleRequest.CurriculumId;

            await _unitOfWork.Modules.UpdateAsync(module);
            await _unitOfWork.CompleteAsync();
            
            return new Response<GetModuleResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
