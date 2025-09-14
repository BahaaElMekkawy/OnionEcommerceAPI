using Microsoft.AspNetCore.Mvc;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Basket;
using OnionEcommerceAPI.Host.Controllers.Base;

namespace OnionEcommerceAPI.Web.Controllers.Basket
{
    public class BasketController : ApiControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public BasketController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket = await _serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _serviceManager.BasketService.DeleteBasketAsync(id);
        }

    }
}
