using OnionEcommerceAPI.Core.Application.Abstractions.Models.Product;

namespace OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Products
{
    public interface IProductService
    {
        public Task<PagedResponse<ProductDetailsDto>> GetAllProductsAsync(string? search, string? sort, int? brandId, int? categoryId, int pageIndex, int pageSize);
        public Task<ProductDetailsDto?> GetProductAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
