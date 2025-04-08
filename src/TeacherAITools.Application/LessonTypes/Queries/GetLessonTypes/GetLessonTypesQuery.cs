using MediatR;
using TeacherAITools.Application.LessonTypes.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.LessonTypes.Queries.GetLessonTypes
{
    public record GetLessonTypesQuery() : IRequest<Response<List<GetLessonTypeResponse>>>;
}
