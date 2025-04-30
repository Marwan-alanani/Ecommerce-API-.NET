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
    // Get which needs authorization
    // GetAll which needs authorization
}