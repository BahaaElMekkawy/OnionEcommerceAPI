using OnionEcommerceAPI.Core.Domain.Entities.Basket;

namespace OnionEcommerceAPI.Core.Domain.Contracts.Infrastructure
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetAsync(string id);
        Task<CustomerBasket?> UpdateAsync(CustomerBasket basket);
        Task<bool> DeleteAsync(string id);
    }
}
