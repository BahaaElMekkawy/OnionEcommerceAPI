using OnionEcommerceAPI.Core.Application.Abstractions.Models.Basket;

namespace OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string id);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);

        Task DeleteBasketAsync(string id);
    }
}
