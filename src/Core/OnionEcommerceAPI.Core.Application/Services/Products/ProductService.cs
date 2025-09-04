using AutoMapper;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Products;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Product;
using OnionEcommerceAPI.Core.Domain.Contracts;
using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Core.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDetailsDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();

            var productsDto = _mapper.Map<IEnumerable<ProductDetailsDto>>(products);

            return productsDto;
        }
        public async Task<ProductDetailsDto?> GetProductAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            if (product is null)
                return null;
            ProductDetailsDto productDto = _mapper.Map<ProductDetailsDto>(product);
            return productDto;

        }
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }
    }
}
