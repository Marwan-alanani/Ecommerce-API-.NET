namespace Presentation.Controllers;

public class PaymentsController(IServiceManager serviceManager)
    : APIController
{
    [HttpPost("{basketId}")]
    public async Task<ActionResult<BasketDTO>> CreateOrUpdate(string basketId)
    {
        return Ok(await serviceManager.PaymentService.CreateOrUpdatePaymentIntent(basketId));
    }

    [HttpPost("Webhook")]
    public async Task<IActionResult> Webhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        await serviceManager.PaymentService.UpdateOrderPaymentStatusAsync(
            json,
            Request.Headers["Stripe-Signature"]!
        );
        // Logic

        return new EmptyResult();
    }
}