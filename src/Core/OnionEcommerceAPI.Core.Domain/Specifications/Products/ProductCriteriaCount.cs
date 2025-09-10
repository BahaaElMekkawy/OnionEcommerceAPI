using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Core.Domain.Specifications.Products
{
    public class ProductCriteriaCount : BaseSpecifications<Product, int>
    {
        public ProductCriteriaCount(string? search, int? brandId, int? categoryId)
                  : base(
                        P => (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
                            &&
                        (!brandId.HasValue || P.BrandId == brandId) &&
                        (!categoryId.HasValue || P.CategoryId == categoryId)
                       )
        {


        }
    }
}
