using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionEcommerceAPI.Core.Domain.Entities.Identity;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Identity.Config
{
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(A => A.Id).ValueGeneratedOnAdd();
            builder.Property(A => A.Street).HasMaxLength(50);
            builder.Property(A => A.City).HasMaxLength(50);
            builder.Property(A => A.Country).HasMaxLength(50);
            builder.Property(A => A.FirstName).HasMaxLength(50);
            builder.Property(A => A.LastName).HasMaxLength(50);

            builder.ToTable("Addresses");
        }
    }
}
