using MediatR;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessonById
{
    public record GetLessonByIdQuery(int id) : IRequest<Response<GetLessonResponse>>;
}