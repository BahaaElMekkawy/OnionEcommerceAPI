using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Basket;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Products;

namespace OnionEcommerceAPI.Core.Application.Abstractions.Contracts
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
    }
}
