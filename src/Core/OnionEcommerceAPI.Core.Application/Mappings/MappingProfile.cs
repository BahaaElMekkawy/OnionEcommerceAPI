using AutoMapper;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Product;
using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Core.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(dest => dest.Brand, O => O.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.Category, O => O.MapFrom(src => src.Category!.Name));

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
        }
    }
}
