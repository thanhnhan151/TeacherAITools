using System.Linq.Expressions;
using TeacherAITools.Domain.Entities.Base.Interfaces;

namespace TeacherAITools.Application.Common.Interfaces.Persistence.Base
{
    public interface IRepository<TEntity> where TEntity : class, IAuditableEntity
    {
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
            bool disableTracking = true);

        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? expression = null);

        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
            bool disableTracking = true);

        Task<TEntity?> GetByIdAsync(object id);

        Task<TEntity> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(object id);

        bool Any();
    }
}
