using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnionEcommerceAPI.Core.Domain.Entities.Identity;
using OnionEcommerceAPI.Infrastructure.Persistence.Identity.Config;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Identity
{
    public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); //Will Configure the 7 Properties from IdentityDbContext
            //builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly); //Will configure all include the the store configurations

            builder.ApplyConfiguration(new ApplicationUserConfigurations());
            builder.ApplyConfiguration(new AddressConfigurations());

        }
    }
}
