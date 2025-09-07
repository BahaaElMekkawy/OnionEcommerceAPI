using AutoMapper;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Products;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Product;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence;
using OnionEcommerceAPI.Core.Domain.Entities.Products;
using OnionEcommerceAPI.Core.Domain.Specifications.Products;

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
            var spec = new ProductWithBrandAndCategorySpecifications();
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var productsDto = _mapper.Map<IEnumerable<ProductDetailsDto>>(products);

            return productsDto;
        }
        public async Task<ProductDetailsDto?> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(spec);
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
