using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Queries.GetCurriculumSubSections
{
    public record GetCurriculumSubSectionsQuery() : IRequest<Response<List<GetCurriculumSubSectionsResponse>>>;
}
