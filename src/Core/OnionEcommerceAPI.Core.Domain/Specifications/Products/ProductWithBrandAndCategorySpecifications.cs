using System.Linq.Expressions;
using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Core.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications(string ? search , string? sort, int? brandId, int? categoryId, int pageIndex ,  int pageSize )
            : base( 
                  P => 
                  (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
                  &&
                  (!brandId.HasValue || P.BrandId == brandId) &&
                  (!categoryId.HasValue || P.CategoryId == categoryId))
        {
            AddIncludes();

            //Criteria = (P => (!brandId.HasValue || P.BrandId == brandId) && (!categoryId.HasValue || P.CategoryId == categoryId));
            switch (sort)
            {
                case "nameDesc":
                    AddOrderByDesc(P => P.Name);
                    break;
                case "priceAsc":
                    //OrderBy = P => P.Price;
                    AddOrderBy(P => P.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Name); // The Default Sorting until been overrides
                    break;
            }
                AddPagination(pageIndex, pageSize);

        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            AddIncludes();
        }
        private protected override void AddIncludes()
        {
            //base.AddIncludes(); //If There is A Shared Include 
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
        private protected override void AddOrderBy(Expression<Func<Product, object>> expression)
        {
            OrderBy = expression;
        }
    }
}
