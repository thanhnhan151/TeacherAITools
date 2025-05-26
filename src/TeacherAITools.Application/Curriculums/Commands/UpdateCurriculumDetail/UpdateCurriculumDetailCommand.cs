using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.UpdateCurriculumDetail
{
public record UpdateCurriculumDetailCommand(
        int Id,
        UpdateCurriculumDetailRequest updateCurriculumRequest) : IRequest<Response<GetDetailCurriculumResponse>>;
}