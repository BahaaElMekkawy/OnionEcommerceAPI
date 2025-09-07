using AutoMapper;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Products;
using OnionEcommerceAPI.Core.Application.Services.Products;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence;

namespace OnionEcommerceAPI.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Lazy<IProductService> _productService;

        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper)  
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = new Lazy<IProductService>(() => new ProductService (_unitOfWork , _mapper));
        }
        public IProductService ProductService => _productService.Value;
    }
}
