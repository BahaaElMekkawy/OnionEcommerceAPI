using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence.DbInitializers;
using OnionEcommerceAPI.Infrastructure.Persistence.Data;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Common
{
    internal abstract class DbInitializer : IDbInitializer
    {
        private readonly DbContext _context;

        protected DbInitializer(DbContext context)
        {
            _context = context;
        }
        public virtual async Task InitializeAsync()
        {
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _context.Database.MigrateAsync();
        }
        public abstract Task SeedAsync();

    }
}
