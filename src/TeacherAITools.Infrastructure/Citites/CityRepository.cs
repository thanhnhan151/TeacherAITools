using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Citites
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
