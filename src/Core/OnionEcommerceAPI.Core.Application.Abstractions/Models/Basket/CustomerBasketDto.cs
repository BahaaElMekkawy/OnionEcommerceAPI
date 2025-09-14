using System.ComponentModel.DataAnnotations;

namespace OnionEcommerceAPI.Core.Application.Abstractions.Models.Basket
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        //**********************************************************************//
        public IEnumerable<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
