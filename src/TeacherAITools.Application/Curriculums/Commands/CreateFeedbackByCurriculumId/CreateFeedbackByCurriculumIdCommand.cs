using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.CreateFeedbackByCurriculumId
{
    public record CreateFeedbackByCurriculumIdCommand(
        int CurriculumId,
        CreateFeedbackRequest Feedback) : IRequest<Response<GetCurriculumResponse>>;
}
