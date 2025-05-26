using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Queries.GetFeedbacksByCurriculumId
{
    public record GetFeedbacksByCurriculumIdQuery(int CurriculumId) : IRequest<Response<List<GetCurriculumFeedbackResponse>>>;
}
