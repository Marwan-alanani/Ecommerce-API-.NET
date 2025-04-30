using Shared.Authentication;

namespace Shared.Orders;

public class OrderResponse
{

    // Id
    public Guid Id { get; set; } // Order id
    public string UserEmail { get; set; } = default!;
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
    public IEnumerable<OrderItemDTO> Items { get; set; } = [];
    public AddressDTO Address { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!;
    public string PaymentStatus { get; set; }
    public string PaymentIntentId { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
}