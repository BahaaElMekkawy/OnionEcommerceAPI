using OnionEcommerceAPI.Core.Application.Abstractions.Models.Product;

namespace OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Products
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDetailsDto>> GetAllProductsAsync();
        public Task<ProductDetailsDto?> GetProductAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
