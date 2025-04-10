using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetUserUpdateById
{
    public record GetUserUpdateByIdQuery(int Id) : IRequest<Response<GetUserUpdateResponse>>;
}
