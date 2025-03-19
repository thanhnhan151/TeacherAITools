using MediatR;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Commands.UpdateSchool
{
    public record UpdateSchoolCommand(
        int Id,
        UpdateSchoolRequest UpdateSchoolRequest) : IRequest<Response<GetSchoolResponse>>;
}
