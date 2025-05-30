using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Commands.DeleteModule
{
    public class DeleteModuleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteModuleCommand, Response<GetModuleResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetModuleResponse>> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleQuery = await _unitOfWork.Modules.GetAsync(expression: m => m.ModuleId == request.Id, disableTracking: true);

            var module = moduleQuery.Include(m => m.Lessons).FirstOrDefault() ?? throw new ApiException(ResponseCode.MODULE_NOT_FOUND);

            if (module.IsActive)
            {
                module.IsActive = false;
                foreach (var lesson in module.Lessons)
                {
                    if (lesson.IsActive) lesson.IsActive = false;
                }
            }
            else
            {
                module.IsActive = true;
                foreach (var lesson in module.Lessons)
                {
                    if (!lesson.IsActive) lesson.IsActive = true;
                }
            }

            await _unitOfWork.Lessons.UpdateRangeAsync(module.Lessons);
            await _unitOfWork.Modules.UpdateAsync(module);
            await _unitOfWork.CompleteAsync();

            return new Response<GetModuleResponse>(code: (int)ResponseCode.DELETED_SUCCESS, message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}