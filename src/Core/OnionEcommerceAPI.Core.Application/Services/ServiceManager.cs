using AutoMapper;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Basket;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Products;
using OnionEcommerceAPI.Core.Application.Services.Basket;
using OnionEcommerceAPI.Core.Application.Services.Products;
using OnionEcommerceAPI.Core.Domain.Contracts.Infrastructure;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence;

namespace OnionEcommerceAPI.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, Func<IBasketService> basketServiceFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory);
        }
        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value;

    }
}
