namespace Domain.Models.OrderModels;

public class Order : BaseEntity<Guid>
{
    public Order()
    {

    }

    public Order(string userEmail, OrderAddress address
        , DeliveryMethod deliveryMethod, decimal subtotal
        , IEnumerable<OrderItem> items)
    {
        UserEmail = userEmail;
        Address = address;
        DeliveryMethod = deliveryMethod;
        Subtotal = subtotal;
        Items = items;
    }

    // Id
    public string UserEmail { get; set; } = default!;
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
    public IEnumerable<OrderItem> Items { get; set; } = [];
    public OrderAddress Address { get; set; } = default!;
    public DeliveryMethod DeliveryMethod { get; set; }
    public int DeliveryMethodId { get; set; }
    public PaymentStatus PaymentStatus { get; set; } =  PaymentStatus.Pending;
    public string PaymentIntentId { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
}