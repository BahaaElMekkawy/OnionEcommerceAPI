using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionEcommerceAPI.Core.Domain.Entities.Products;
using OnionEcommerceAPI.Infrastructure.Persistence.Data.Configurations.Base;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Data.Configurations.Products
{
    public class BrandConfigurations : BaseEntityConfigurations<ProductBrand,int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
