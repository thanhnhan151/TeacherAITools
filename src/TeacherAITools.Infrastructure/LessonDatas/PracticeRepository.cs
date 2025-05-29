using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.LessonDatas
{
    public class PracticeRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<Practice>(dbContext, logger), IPracticeRepository
    {
    }
}
