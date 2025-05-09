global using Shared.DataTransferObjects.Basket;

namespace ServicesAbstraction;

public interface IPaymentService
{
    Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId);
    Task UpdateOrderPaymentStatusAsync(string jsonRequest ,  string stripeHeader);

}