using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionEcommerceAPI.Core.Domain.Entities.Products;
using OnionEcommerceAPI.Infrastructure.Persistence.Data.Configurations.Base;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Data.Configurations.Products
{
    public class CategoryConfigurations : BaseAuditableEntityConfigurations<ProductCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder.Property(C => C.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
