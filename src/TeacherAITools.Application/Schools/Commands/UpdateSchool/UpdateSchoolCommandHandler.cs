using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Commands.UpdateSchool
{
    public class UpdateSchoolCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateSchoolCommand, Response<GetSchoolResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetSchoolResponse>> Handle(UpdateSchoolCommand request, CancellationToken cancellationToken)
        {
            var schoolQuery = await _unitOfWork.Schools.GetAsync(expression: u => u.SchoolId == request.Id, disableTracking: true);

            var school = schoolQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.SCHOOL_NOT_FOUND);

            school.Name = request.UpdateSchoolRequest.Name;
            school.Description = request.UpdateSchoolRequest.Description;
            school.ImageURL = request.UpdateSchoolRequest.ImageURL;

            await _unitOfWork.Schools.UpdateAsync(school);
            await _unitOfWork.CompleteAsync();

            return new Response<GetSchoolResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
