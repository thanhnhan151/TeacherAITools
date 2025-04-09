using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Periods
{
    public class PeriodRepository(
       TeacherAIToolsDbContext dbContext,
       ILogger logger) : Repository<Period>(dbContext, logger), IPeriodRepository
    {
        //public int GetLastIdPeriod()
        //{
        //    var period = _dbContext.Periods.OrderBy(e => e.Id).LastOrDefault();
        //    return period is not null ? period.Id : 0;
        //}

        //public async Task<bool> IsCompleteForTeacherAsync(int lessonId, int userId) => await _dbContext.Periods.AnyAsync(p => p.LessonId == lessonId && p.UserId == userId);
    }
}