using Shared.Orders;

namespace ServicesAbstraction;

public interface IOrderService
{
    // Create()

    Task<OrderResponse> CreateAsync(OrderRequest request, string email);
    Task<OrderResponse> GetAsync(Guid Id);
    Task<IEnumerable<OrderResponse>> GetAllAsync(string email);
    Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync();
}