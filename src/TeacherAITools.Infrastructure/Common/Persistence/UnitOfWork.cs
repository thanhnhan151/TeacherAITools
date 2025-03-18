using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Infrastructure.Curriculums;
using TeacherAITools.Infrastructure.Modules;
using TeacherAITools.Infrastructure.Users;

namespace TeacherAITools.Infrastructure.Common.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TeacherAIToolsDbContext _dbContext;

        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }

        public IModuleRepository Modules { get; private set; }

        public ICurriculumRepository Curriculums { get; private set; }

        public UnitOfWork(
            TeacherAIToolsDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;

            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(_dbContext, _logger);
            Modules = new ModuleRepository(_dbContext, _logger);
            Curriculums = new CurriculumRepository(_dbContext, _logger);
        }

        public async Task CompleteAsync() => await _dbContext.SaveChangesAsync();
    }
}
