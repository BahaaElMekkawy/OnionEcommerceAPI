using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OnionEcommerceAPI.Core.Domain.Contracts;
using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Data
{
    internal class StoreContextInitializer : IStoreContextInitializer
    {
        private readonly StoreContext _storeContext;

        public StoreContextInitializer(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task InitializeAsync()
        {
            var pendingMigrations = await _storeContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _storeContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            #region Brands Seeding
            if (!_storeContext.Brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync(@"D:\OnionEcommerceAPI\src\Infrastructure\OnionEcommerceAPI.Infrastructure.Persistence\Data\Seeds\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (!(brands?.Count > 0))
                {
                    await _storeContext.Brands.AddRangeAsync(brands);
                    await _storeContext.SaveChangesAsync();
                }
            }
            #endregion

            #region Categories Seeding
            if (!_storeContext.Categories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync(@"D:\OnionEcommerceAPI\src\Infrastructure\OnionEcommerceAPI.Infrastructure.Persistence\Data\Seeds\categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count > 0)
                {
                    await _storeContext.Categories.AddRangeAsync(categories);
                    await _storeContext.SaveChangesAsync();
                }
            }
            #endregion

            #region Products Seeding
            if (!_storeContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(@"D:\OnionEcommerceAPI\src\Infrastructure\OnionEcommerceAPI.Infrastructure.Persistence\Data\Seeds\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    await _storeContext.Products.AddRangeAsync(products);
                    await _storeContext.SaveChangesAsync();
                }
            }
            #endregion       
        }
    }
}
