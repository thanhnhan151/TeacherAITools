using System.Data.Common;
using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Modules
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(
            TeacherAIToolsDbContext dbContext
            , ILogger logger) : base(dbContext, logger)
        {
        }
    }
}