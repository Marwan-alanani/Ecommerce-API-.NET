namespace Domain.Models.Basket;

public class CustomerBasket
{
    public string Id { get; set; } // Created from Client
    public ICollection<BasketItem> BasketItems { get; set; } = [];
}