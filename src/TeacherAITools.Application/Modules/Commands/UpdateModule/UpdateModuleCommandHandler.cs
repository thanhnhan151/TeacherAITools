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
            if(!_unitOfWork.Curriculums.Any(
                x => x.CurriculumId == request.updateModuleRequest.CurriculumId)){
                    throw new ApiException(ResponseCode.CURRICULUM_NOT_FOUND);
            }

            if(!_unitOfWork.Grades.Any(
                x => x.GradeId == request.updateModuleRequest.GradeId)){
                    throw new ApiException(ResponseCode.ID_GRADE_DONT_EXIST);
            }

            if(!_unitOfWork.Books.Any(
                x => x.BookId == request.updateModuleRequest.BookId)){
                    throw new ApiException(ResponseCode.ID_BOOK_DONT_EXIST);
            }

            var moduleQuery = await _unitOfWork.Modules.GetAsync(expression: m => m.ModuleId == request.Id, disableTracking: true);

            var module = moduleQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.MODULE_NOT_FOUND);

            module.Name = request.updateModuleRequest.Name;
            module.Desciption = request.updateModuleRequest.Desciption;
            module.Semester = request.updateModuleRequest.Semester;
            module.TotalPeriods = request.updateModuleRequest.TotalPeriods;
            module.CurriculumId = request.updateModuleRequest.CurriculumId;
            module.GradeId = request.updateModuleRequest.GradeId;
            module.BookId = request.updateModuleRequest.BookId;

            await _unitOfWork.Modules.UpdateAsync(module);
            await _unitOfWork.CompleteAsync();
            
            return new Response<GetModuleResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
