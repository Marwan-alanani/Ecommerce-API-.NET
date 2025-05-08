namespace Shared.DataTransferObjects.Basket;

public record BasketDTO
{
   public string Id { get; init; }
   public ICollection<BasketItemDTO> Items { get; init; } = [];
   public string? ClientSecret { get; set; }
   public string? PaymentIntentId { get; set; }
   public int? DeliveryMethodId { get; set; }
   public decimal? ShippingPrice { get; set; }
}