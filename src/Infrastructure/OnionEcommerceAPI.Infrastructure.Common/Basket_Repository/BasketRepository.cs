using System.Text.Json;
using OnionEcommerceAPI.Core.Domain.Contracts.Infrastructure;
using OnionEcommerceAPI.Core.Domain.Entities.Basket;
using StackExchange.Redis;

namespace OnionEcommerceAPI.Infrastructure.Common.Basket_Repository
{
    internal class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket)
        {
            var value = JsonSerializer.Serialize(basket);
            var updated = await _database.StringSetAsync(basket.Id, value, TimeSpan.FromDays(15)); //return bool if it is updated
            if (updated)
                return basket;
            return null;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}
