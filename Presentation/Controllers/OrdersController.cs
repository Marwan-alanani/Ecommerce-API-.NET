using Microsoft.AspNetCore.Authorization;
using Shared.Orders;

namespace Presentation.Controllers;

[Authorize]
public class OrdersController(IServiceManager service)
    : APIController
{
    // Create(address, basketId, deliveryMethodId) => OrderResponse
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
    {

        return Ok(await service.OrderService.CreateAsync(request, GetEmailFromToken()));
    }
    // GetDeliveryMethods()
    [HttpGet("deliveryMethods")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetDeliveryMethods()
    {
        return Ok(await service.OrderService.GetDeliveryMethodsAsync());
    }


    // Get which needs authorization
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> Get(Guid id)
    {
        return Ok(await service.OrderService.GetAsync(id));
    }
    // GetAll which needs authorization
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAll()
    {
        return Ok(await service.OrderService.GetAllAsync(GetEmailFromToken()));
    }
}