namespace Services;

internal class BasketService(IBasketRespository basketRepository, IMapper mapper)
    : IBasketService
{
    public async Task<BasketDTO> GetAsync(string id)
    {
        var basket = await basketRepository.GetAsync(id) ?? throw new BasketNotFoundException(id);
        return mapper.Map<BasketDTO>(basket);
    }

    public async Task<BasketDTO> UpdateAsync(BasketDTO basket)
    {
        var customerBasket = mapper.Map<CustomerBasket>(basket);
        var updatedBasket = await basketRepository.UpdateAsync(customerBasket) ??
                            throw new Exception($"Can't update basket Now !!");
        return mapper.Map<BasketDTO>(updatedBasket);
    }

    public async Task<bool> DeleteAsync(string id) =>
        await basketRepository.DeleteAsync(id);
}