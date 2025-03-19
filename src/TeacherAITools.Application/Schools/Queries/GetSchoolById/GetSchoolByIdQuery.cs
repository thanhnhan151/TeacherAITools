using MediatR;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Queries.GetSchoolById
{
    public record GetSchoolByIdQuery(int SchoolId) : IRequest<Response<GetSchoolResponse>>;
}
