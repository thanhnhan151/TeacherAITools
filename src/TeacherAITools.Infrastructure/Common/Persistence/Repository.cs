﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities.Base.Implementations;

namespace TeacherAITools.Infrastructure.Common.Persistence
{
    public class Repository<TEntity>(
        TeacherAIToolsDbContext dbContext,
        ILogger logger) : IRepository<TEntity> where TEntity : class
    {
        protected TeacherAIToolsDbContext _dbContext = dbContext;
        protected readonly ILogger _logger = logger;

        public Task<IQueryable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>,
            IQueryable<TEntity>>? includeFunc = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (disableTracking) query = query.AsNoTracking();

            if (expression != null) query = query.Where(expression);

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }

            return Task.FromResult(query.AsQueryable());
        }

        public Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return Task.FromResult(_dbContext.Set<TEntity>().Where(expression).AsQueryable());
        }

        public Task<IQueryable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (disableTracking) query = query.AsNoTracking();

            if (expression != null) query = query.Where(expression);

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }

            return Task.FromResult(orderBy != null ? orderBy(query).AsQueryable() : query.AsQueryable());
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached) _dbContext.Set<TEntity>().Attach(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;

            _dbContext.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public bool Any()
        {
            throw new NotImplementedException();
        }

        public async Task<BasePaginationEntity<TEntity>> PaginationAsync(
            int page = 0,
            int pageSize = 20,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFunc = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            var total = await query.CountAsync(cancellationToken);

            query = query.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var data = await query.ToListAsync(cancellationToken);

            return new BasePaginationEntity<TEntity>() { Data = data, Total = total };
        }
    }
}
