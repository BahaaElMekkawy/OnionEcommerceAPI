using AutoMapper;
using Microsoft.Extensions.Configuration;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Product;
using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Core.Application.Mappings
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDetailsDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty((source.PictureUrl)))
                return $"{_configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";
            return string.Empty;
        }
    }
}
