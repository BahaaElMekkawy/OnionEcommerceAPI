using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence;
using OnionEcommerceAPI.Infrastructure.Persistence.Data;
using OnionEcommerceAPI.Infrastructure.Persistence.Interceptors;

namespace OnionEcommerceAPI.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>((optionBuilder) => //using the ctor that takes Action of optionsBuilder and set the lifetime to scoped by default
            {
                optionBuilder.UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });

            services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
            services.AddScoped<ISaveChangesInterceptor, CustomSaveChangesInterceptor>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            return services;
        }
    }
}
