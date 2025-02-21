using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;

namespace TeacherAITools.Infrastructure.Common.Persistence
{
    public class Repository<TEntity>(
        TeacherAIToolsDbContext dbContext,
        ILogger logger) : IRepository<TEntity> where TEntity : class
    {
        protected TeacherAIToolsDbContext _dbContext = dbContext;
        protected DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
        protected readonly ILogger _logger = logger;

        public Task AddEntitiesAsync(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TEntity>> GetAllEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetEntityByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
