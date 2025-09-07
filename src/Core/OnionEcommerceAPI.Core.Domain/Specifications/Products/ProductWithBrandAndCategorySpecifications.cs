using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Core.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
