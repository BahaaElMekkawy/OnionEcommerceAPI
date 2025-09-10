using Microsoft.AspNetCore.Mvc;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Product;
using OnionEcommerceAPI.Host.Controllers.Base;

namespace OnionEcommerceAPI.Web.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResponse<ProductDetailsDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(specParams.Search, specParams.Sort,specParams.BrandId, specParams.CategoryId , specParams.PageIndex , specParams.PageSize );
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDetailsDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await serviceManager.ProductService.GetAllCategoriesAsync();
            return Ok(categories);
        }

    }
}
