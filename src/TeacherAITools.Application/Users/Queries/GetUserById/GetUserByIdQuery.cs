using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int UserId) : IRequest<Response<GetUserResponse>>;
}
