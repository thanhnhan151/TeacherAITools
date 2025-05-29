using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.LessonDatas
{
    public class ApplyRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<Apply>(dbContext, logger), IApplyRepository
    {
    }
}
