using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Commands.DeleteModule
{
    public class DeleteModuleCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetModuleResponse>> Handle(int request, CancellationToken cancellationToken)
        {
            var moduleQuery = await _unitOfWork.Modules.GetAsync(expression: m => m.ModuleId == request, disableTracking: true);

            var module = moduleQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.MODULE_NOT_FOUND);

            await _unitOfWork.Modules.UpdateAsync(module);
            await _unitOfWork.CompleteAsync();

            return new Response<GetModuleResponse>(code: (int)ResponseCode.DELETED_SUCCESS, message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}