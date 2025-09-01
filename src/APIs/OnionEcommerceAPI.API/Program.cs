using Microsoft.EntityFrameworkCore;
using OnionEcommerceAPI.API.Extensions;
using OnionEcommerceAPI.Core.Domain.Contracts;
using OnionEcommerceAPI.Infrastructure.Persistence;
using OnionEcommerceAPI.Infrastructure.Persistence.Data;

namespace OnionEcommerceAPI.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            //DependencyInjection.AddPersistenceServices(webApplicationBuilder.Services , webApplicationBuilder.Configuration); call from the static class
            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration); //using as extension method

            #endregion

            var app = webApplicationBuilder.Build();

            #region Database Intializations
            await app.InitializeStoreContextAsync();
            #endregion

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
