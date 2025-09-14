using AutoMapper;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Basket;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Basket;
using OnionEcommerceAPI.Core.Application.Common.Exception;
using OnionEcommerceAPI.Core.Domain.Contracts.Infrastructure;
using OnionEcommerceAPI.Core.Domain.Entities.Basket;

namespace OnionEcommerceAPI.Core.Application.Services.Basket
{
    internal class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<CustomerBasketDto> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetAsync(id);
            if (basket is null)
                throw new NotFoundException(nameof(Basket), id);
            var basketDto = _mapper.Map<CustomerBasketDto>(basket);
            return basketDto;

        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);

            var updatedBasket = await _basketRepository.UpdateAsync(basket);
            if (updatedBasket is null)
                throw new BadRequestException("Failed to Updated the Basket");

            //return _mapper.Map<CustomerBasketDto>(updatedBasket);
            return basketDto;// Mapping is useLess because it will be the same as the passed basketDto. 
        }
        public async Task DeleteBasketAsync(string id)
        {
            var deleted = await _basketRepository.DeleteAsync(id);
            if (!deleted)
                throw new BadRequestException("Failed To Delete The Basket");
        }
    }
}
