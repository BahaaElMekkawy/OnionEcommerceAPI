using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionEcommerceAPI.Core.Domain.Entities.Identity;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Identity.Config
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(A => A.DisplayName)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.HasOne(A => A.Address)
                .WithOne(D => D.User)
                .HasForeignKey<Address>(A => A.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
