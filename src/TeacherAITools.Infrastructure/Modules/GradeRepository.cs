using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Modules
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
