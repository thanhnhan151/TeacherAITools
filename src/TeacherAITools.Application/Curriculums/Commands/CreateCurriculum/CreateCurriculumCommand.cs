using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.CreateCurriculum{
    public record CreateCurriculumCommand(
        CreateCurriculumRequest createCurriculumRequest) : IRequest<Response<GetCurriculumResponse>>;
}