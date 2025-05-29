using MediatR;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessonInfoById
{
    public record GetLessonInfoByIdQuery(int Id) : IRequest<Response<GetLessonInfoResponse>>;
}
