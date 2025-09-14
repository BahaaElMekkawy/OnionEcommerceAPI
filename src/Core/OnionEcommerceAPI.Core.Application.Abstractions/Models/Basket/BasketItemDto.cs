using System.ComponentModel.DataAnnotations;

namespace OnionEcommerceAPI.Core.Application.Abstractions.Models.Basket
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        //**********************************************************************//
        [Required]
        public required string Name { get; set; }
        //**********************************************************************//
        public string? PictureUrl { get; set; }
        //**********************************************************************//
        [Required]
        [Range(0.1,double.MaxValue , ErrorMessage = "Price Must Be Greater Than 0")]
        public decimal Price { get; set; }
        //**********************************************************************//
        [Required]
        [Range(1,int.MaxValue , ErrorMessage = "Quantity Must 1 or More  ")]
        public int Quantity { get; set; }
        //**********************************************************************//
        public string? Brand { get; set; }
        //**********************************************************************//
        public string? Category { get; set; }
    } 
}
 