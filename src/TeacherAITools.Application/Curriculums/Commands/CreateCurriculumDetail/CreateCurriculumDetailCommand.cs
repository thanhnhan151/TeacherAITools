using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.CreateCurriculumDetail
{
    public record CreateCurriculumDetailCommand(
        CreateCurriculumDetailRequest createCurriculumDetailRequest) : IRequest<Response<GetDetailCurriculumResponse>>;
}