using Domain.Models.OrderModels;
using Shared.Orders;

namespace Services;

public class OrderService(
    IMapper mapper,
    IUnitOfWork _unitOfWork,
    IBasketRespository _basketRespository)
    : IOrderService
{
    public async Task<OrderResponse> CreateAsync(OrderRequest request, string email)
    {
        var basket = await _basketRespository.GetAsync(request.BasketId) ??
                     throw new BasketNotFoundException(request.BasketId);
        var productRepo = _unitOfWork.GetRepository<Product>();

        List<OrderItem> items = [];
        foreach (var item in basket.BasketItems)
        {
            var product = await productRepo.GetAsync(item.Id) ??
                          throw new ProductNotFoundException(item.Id);
            items.Add(CreateOrderItem(product, item));
            item.Price = product.Price;
        }

        var address = mapper.Map<OrderAddress>(request.Address);
        var method = await _unitOfWork.GetRepository<DeliveryMethod>().GetAsync(request.DeliveryMethodId) ??
                     throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

        var subtotal = items.Sum(item => item.Price * item.Quantity);
        var order = new Order(email, items, address, method, subtotal);
        _unitOfWork.GetRepository<Order, Guid>()
            .Add(order);
        await _unitOfWork.SaveChangesAsync();
        return mapper.Map<OrderResponse>(order);
    }

    private static OrderItem CreateOrderItem(Product product, BasketItem item)
    {
        return new OrderItem(new(product.Id, product.Name, product.PictureUrl)
            , product.Price
            , item.Quantity);
    }

    public async Task<OrderResponse> GetAsync(Guid Id)
    {
        var orders = await _unitOfWork.GetRepository<Order, Guid>()
            .GetAsync( new OrderSpecifications(Id));
        return mapper.Map<OrderResponse>(orders);
    }

    public async Task<IEnumerable<OrderResponse>> GetAllAsync(string email)
    {
        var orders = await _unitOfWork.GetRepository<Order, Guid>()
            .GetAllAsync( new OrderSpecifications(email));

        return mapper.Map<IEnumerable<OrderResponse>>(orders);

    }

    public async Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync()
    {
        var deliveryMethods  = await _unitOfWork.GetRepository<DeliveryMethod>().GetAllAsync();
        return mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
 }
}