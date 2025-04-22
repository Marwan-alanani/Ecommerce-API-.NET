namespace Domain.Models.Basket;

public class BasketItem
{
    public int Id { get; set; } // => Product id
    public string ProductName { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public Decimal Price { get; set; }
    public int Quantity { get; set; }
}