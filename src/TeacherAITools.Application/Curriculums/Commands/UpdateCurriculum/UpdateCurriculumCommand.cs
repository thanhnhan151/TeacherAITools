using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.UpdateCurriculum
{
    public record UpdateCurriculumCommand(
        int Id,
        UpdateCurriculumRequest updateCurriculumRequest) : IRequest<Response<GetCurriculumResponse>>;
}
