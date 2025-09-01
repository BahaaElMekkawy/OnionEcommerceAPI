using OnionEcommerceAPI.Core.Domain.Contracts;

namespace OnionEcommerceAPI.API.Extensions
{
    public  static class InitializerExtensions
    {
        public async static Task<WebApplication> InitializeStoreContextAsync(this WebApplication app) 
        {
            using var scope = app.Services.CreateAsyncScope(); //Using to dispose after usage 
            var services = scope.ServiceProvider;

            var storeDbContextInitializer = services.GetRequiredService<IStoreContextInitializer>();
            //Ask Runtime to get an object of "StoreContext" Explicitly , Implicitly may be done with the property injection

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await storeDbContextInitializer.InitializeAsync();
                await storeDbContextInitializer.SeedAsync();
            }
            catch (Exception ex)
            {
                //to prevent app from crashing if there is an exception from the migrations
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Happened During Migrations");
            }
            return app;
        }
    }   
}
