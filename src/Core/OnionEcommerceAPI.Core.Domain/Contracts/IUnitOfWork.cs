using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Core.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey> 
            where TKey : IEquatable<TKey>;

        Task<int> CompleteAsync(); 
    }
}
