global using Product = Domain.Models.Products.Product;
using Stripe;

namespace Services;

public class PaymentService(
    IBasketRespository basketRepository,
    IUnitOfWork unitOfWork,
    IConfiguration configuration,
    IMapper mapper)
    : IPaymentService
{
    public async Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId)
    {
        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        // or
        // StripeConfiguration.ApiKey = configuration.GetSection("Stripe")["SecretKey"];
        var basket = await basketRepository.GetAsync(basketId) ??
                     throw new BasketNotFoundException(basketId);
        var productRepo = unitOfWork.GetRepository<Product>();
        foreach (var item in basket.Items)
        {
            var product = await productRepo.GetAsync(item.Id) ??
                          throw new ProductNotFoundException(item.Id);
            item.Price = product.Price;
        }

        ArgumentNullException.ThrowIfNull(basket.DeliveryMethodId);

        var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod>()
                                 .GetAsync(basket.DeliveryMethodId.Value)
                             ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

        basket.ShippingPrice = deliveryMethod.Cost;
        var amount = (long)(basket.Items.Sum(item => item.Price * item.Quantity) + deliveryMethod.Cost) * 100;

        // Create or Update paymentIntent
        var service = new PaymentIntentService();
        if (string.IsNullOrWhiteSpace(basket.PaymentIntentId)) // Create
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "USD",
                PaymentMethodTypes = ["card"]
            };
            var paymentIntent = await service.CreateAsync(options);
            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;
        }
        else // Update
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = amount,
            };
            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        await basketRepository.UpdateAsync(basket);
        return mapper.Map<BasketDTO>(basket);


        // Basket Repo =>  Get Basket
        // unit of work to get delivery method and products
        // configuration
        // mapper
    }
}