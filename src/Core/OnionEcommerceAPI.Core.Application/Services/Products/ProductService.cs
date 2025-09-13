using AutoMapper;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Products;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Product;
using OnionEcommerceAPI.Core.Application.Common.Exception;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence;
using OnionEcommerceAPI.Core.Domain.Entities.Products;
using OnionEcommerceAPI.Core.Domain.Specifications.Products;

namespace OnionEcommerceAPI.Core.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResponse<ProductDetailsDto>> GetAllProductsAsync(string? search , string? sort, int? brandId, int? categoryId, int pageIndex, int pageSize)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(search ,sort, brandId, categoryId, pageIndex, pageSize);

            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var productsDto = _mapper.Map<IEnumerable<ProductDetailsDto>>(products);

            var countSpec = new ProductCriteriaCount(search , brandId, categoryId);

            var totalCount = await _unitOfWork.GetRepository<Product, int>().GetCountAsync(countSpec);

            var response = new PagedResponse<ProductDetailsDto>()
            {
                Data = productsDto,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return response;
        }
        public async Task<ProductDetailsDto?> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if (product is null)
                throw new NotFoundException(nameof(Product) , id);
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
