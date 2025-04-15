using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Queries.GetCurriculumById
{
    public record GetCurriculumByIdQuery(int Id) : IRequest<Response<GetDetailCurriculumResponse>>;
}