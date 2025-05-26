using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class CurriculumFeedbackRepository(
        TeacherAIToolsDbContext dbContext
            , ILogger logger) : Repository<CurriculumFeedback>(dbContext, logger), ICurriculumFeedbackRepository
    {
    }
}
