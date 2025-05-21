
using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.DeleteCurriculum
{
    public record DeleteCurriculumCommand(int Id) : IRequest<Response<GetCurriculumResponse>>;
}