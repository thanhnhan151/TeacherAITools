﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Domain.Entities.Base.Interfaces;

namespace TeacherAITools.Infrastructure.Common.Persistence
{
    public class AuditableEntitiesInterceptor(
        ILogger<AuditableEntitiesInterceptor> logger,
        IDateTimeProvider dateTimeProvider,
        ICurrentUserService currentUserService) : SaveChangesInterceptor
    {
        private readonly ILogger<AuditableEntitiesInterceptor> _logger = logger;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateAuditableEntities(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditableEntities(DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.CurrentPrincipal;
                        entry.Entity.CreatedAt = _dateTimeProvider.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.CurrentPrincipal;
                        entry.Entity.UpdatedAt = _dateTimeProvider.UtcNow;
                        break;
                    default:
                        continue;
                }
            }
        }
    }
}
