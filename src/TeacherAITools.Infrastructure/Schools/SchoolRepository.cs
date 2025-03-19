using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Schools
{
    public class SchoolRepository(
        TeacherAIToolsDbContext dbContext,
        ILogger logger) : Repository<School>(dbContext, logger), ISchoolRepository
    {
    }
}
