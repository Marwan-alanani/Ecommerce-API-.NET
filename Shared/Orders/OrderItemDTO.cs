namespace Shared.Orders;

public record OrderItemDTO
{

    public string PictureUrl { get; set; }
    public string ProductName { get; set; }
    public Decimal Price { get; set; }
    public int Quantity { get; set; }
}