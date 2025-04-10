using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetTeacherLessonsById
{
    public record GetTeacherLessonsByIdQuery(int Id) : IRequest<Response<List<GetUserLessonsResponse>>>;
}
