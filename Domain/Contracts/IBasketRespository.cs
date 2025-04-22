using Domain.Models.Basket;

namespace Domain.Contracts;

public interface IBasketRespository
{
    Task<CustomerBasket?> GetAsync(string id);
    Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan? timeToLive = null); // TTL
    Task<bool> DeleteAsync(string id);
}