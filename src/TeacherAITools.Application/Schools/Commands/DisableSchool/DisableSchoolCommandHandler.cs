using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Commands.DisableSchool
{
    public class DisableSchoolCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DisableSchoolCommand, Response<GetSchoolResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetSchoolResponse>> Handle(DisableSchoolCommand request, CancellationToken cancellationToken)
        {
            var schoolQuery = await _unitOfWork.Schools.GetAsync(expression: u => u.SchoolId == request.Id, disableTracking: true);

            var school = schoolQuery.Include(s => s.Users.Where(u => u.RoleId != (int)AvailableRole.Administrator)).FirstOrDefault() ?? throw new ApiException(ResponseCode.USER_NOT_FOUND);

            if (school.Status)
            {
                school.Status = false;
                foreach (var user in school.Users)
                {
                    if (user.IsActive) user.IsActive = false;
                }
            }
            else
            {
                school.Status = true;
                foreach (var user in school.Users)
                {
                    if (!user.IsActive) user.IsActive = true;
                }
            }

            await _unitOfWork.Schools.UpdateAsync(school);

            await _unitOfWork.CompleteAsync();

            return new Response<GetSchoolResponse>(code: (int)ResponseCode.DISABLED_SUCCESS, message: ResponseCode.DISABLED_SUCCESS.GetDescription());
        }
    }
}
