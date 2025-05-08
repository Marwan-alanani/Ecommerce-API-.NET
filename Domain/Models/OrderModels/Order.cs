namespace Domain.Models.OrderModels;

public class Order : BaseEntity<Guid>
{
    public Order()
    {
    }

    public Order(string buyerEmail, IEnumerable<OrderItem> items, OrderAddress shipToAddress
        , DeliveryMethod deliveryMethod, decimal subtotal
        , string paymentIntentId)
    {
        BuyerEmail = buyerEmail;
        ShipToAddress = shipToAddress;
        DeliveryMethod = deliveryMethod;
        Subtotal = subtotal;
        Items = items;
        PaymentIntentId = paymentIntentId;
    }

    // Id
    public string BuyerEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public IEnumerable<OrderItem> Items { get; set; } = new List<OrderItem>();
    public OrderAddress ShipToAddress { get; set; } = default!;
    public DeliveryMethod DeliveryMethod { get; set; }
    public int DeliveryMethodId { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string PaymentIntentId { get; set; } = default!;
    public decimal Subtotal { get; set; }
}