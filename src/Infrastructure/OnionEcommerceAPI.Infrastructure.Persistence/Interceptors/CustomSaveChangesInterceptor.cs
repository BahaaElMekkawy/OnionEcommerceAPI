using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;
using OnionEcommerceAPI.Core.Domain.Common;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Interceptors
{
    public class CustomSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public CustomSaveChangesInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context) 
        {
            if (context is null)
                return;
            var changes = context.ChangeTracker.Entries<BaseAuditableEntity<int>>()
                .Where(e => e.State is EntityState.Modified or EntityState.Added);

            foreach (var entry in changes)
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = _currentUserService.UserId!;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }
                //else is modified so the modified fields will only be set and the addition has been set before
                entry.Entity.LastModifiedBy = _currentUserService.UserId!;
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
