namespace OnionEcommerceAPI.Core.Application.Abstractions.Models.Product
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string? PictureUrl { get; set; }
        public required decimal Price { get; set; }
        public int? BrandId { get; set; }
        public virtual string? Brand { get; set; }
        public int? CategoryId { get; set; }
        public virtual string? Category { get; set; }
    }
}
