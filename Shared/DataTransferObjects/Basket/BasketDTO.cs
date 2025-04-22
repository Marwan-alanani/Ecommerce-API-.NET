namespace Shared.DataTransferObjects.Basket;

public record BasketDTO
{
   public string Id { get; init; }
   public ICollection<BasketDTO> BasketItems { get; init; } = [];
}