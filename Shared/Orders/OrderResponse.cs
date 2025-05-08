using Shared.Authentication;

namespace Shared.Orders;

public class OrderResponse
{

    // Id
    public Guid Id { get; set; } // Order id
    public string BuyerEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public AddressDTO ShipToAddress { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!;
    public decimal  DeliveryCost { get; set; }
    public IEnumerable<OrderItemDTO> Items { get; set; } = [];
    public decimal Subtotal { get; set; }
    public string Status { get; set; }
    public decimal Total { get; set; }
    public string PaymentIntentId { get; set; } = string.Empty;
}