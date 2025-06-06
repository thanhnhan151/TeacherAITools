using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Periods
{
    public class PeriodDetailRepository(
       TeacherAIToolsDbContext dbContext,
       ILogger logger) : Repository<PeriodDetail>(dbContext, logger), IPeriodDetailRepository
    {
    }
}