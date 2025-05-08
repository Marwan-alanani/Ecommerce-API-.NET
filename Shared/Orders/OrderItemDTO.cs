namespace Shared.Orders;

public record OrderItemDTO
{

    public int ProductId { get; set; }
    public string PictureUrl { get; set; }
    public string ProductName { get; set; }
    public Decimal Price { get; set; }
    public int Quantity { get; set; }
}