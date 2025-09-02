using OnionEcommerceAPI.Core.Domain.Common;
using OnionEcommerceAPI.Core.Domain.Contracts;
using OnionEcommerceAPI.Infrastructure.Persistence.Data;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
            if (withTracking)
                return await _context.Set<TEntity>().ToListAsync();
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
