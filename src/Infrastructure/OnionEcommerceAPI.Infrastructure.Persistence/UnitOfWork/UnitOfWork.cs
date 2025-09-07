using System.Collections.Concurrent;
using OnionEcommerceAPI.Core.Domain.Common;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence;
using OnionEcommerceAPI.Infrastructure.Persistence.Data;
using OnionEcommerceAPI.Infrastructure.Persistence.Repositories.Generic_Repository;

namespace OnionEcommerceAPI.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private readonly ConcurrentDictionary<string, object> repositories;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
            repositories = new ConcurrentDictionary<string, object>();
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var repoName = typeof(TEntity).Name;
            return (IGenericRepository<TEntity, TKey>)repositories.GetOrAdd(repoName, new GenericRepository<TEntity, TKey>(_context));
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public ValueTask DisposeAsync() => _context.DisposeAsync();

    }
}
