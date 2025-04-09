using MediatR;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessonById
{
    public record GetTeacherLessonByIdQuery(int Id) : IRequest<Response<GetDetailTeacherLessonResponse>>;
}
