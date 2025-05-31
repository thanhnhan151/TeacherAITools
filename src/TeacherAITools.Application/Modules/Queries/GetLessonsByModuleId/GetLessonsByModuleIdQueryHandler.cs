using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Queries.GetLessonsByModuleId
{
    public class GetLessonsByModuleIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetLessonsByModuleIdQuery, Response<GetModuleDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetModuleDetailResponse>> Handle(GetLessonsByModuleIdQuery request, CancellationToken cancellationToken)
        {
            var moduleQuery = await _unitOfWork.Modules.GetAsync(m => m.ModuleId == request.ModuleId);

            if (request.RoleId == (int)AvailableRole.Teacher)
            {
                var teacherModule = moduleQuery.Include(m => m.Lessons.Where(l => l.IsActive).OrderBy(l => l.LessonId)).FirstOrDefault() ?? throw new ApiException(ResponseCode.MODULE_NOT_FOUND);

                return new Response<GetModuleDetailResponse>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<GetModuleDetailResponse>(teacherModule),
                message: ResponseCode.SUCCESS.GetDescription());
            }

            var module = moduleQuery.Include(m => m.Lessons).FirstOrDefault() ?? throw new ApiException(ResponseCode.MODULE_NOT_FOUND);

            return new Response<GetModuleDetailResponse>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<GetModuleDetailResponse>(module),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
