using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers;

[Authorize]
public class OrdersController
    : APIController
{
    // Create(address, basketId, deliveryMethodId) => OrderResponse
    // GetDeliveryMethods()
    // Get which needs authorization
    // GetAll which needs authorization
}