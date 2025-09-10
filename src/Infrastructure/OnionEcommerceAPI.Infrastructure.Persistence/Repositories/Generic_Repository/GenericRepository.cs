using OnionEcommerceAPI.Core.Domain.Common;
using OnionEcommerceAPI.Core.Domain.Contracts;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence;
using OnionEcommerceAPI.Infrastructure.Persistence.Data;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Repositories.Generic_Repository
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
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications, bool withTracking = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (!withTracking)
                query = query.AsNoTracking();

            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(query, specifications).ToListAsync();
        }
        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specifications).CountAsync();
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
