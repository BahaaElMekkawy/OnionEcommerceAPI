using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Data
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly, type =>
                type.Namespace.Contains("OnionEcommerceAPI.Infrastructure.Persistence.Data.Configurations"));
            ;
        }

    }
}
