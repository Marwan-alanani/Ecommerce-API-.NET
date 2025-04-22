using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Basket;

public record BasketItemDTO
{
    public int Id { get; set; } // => Product id
    public string ProductName { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    [Range(1 , double.MaxValue)]
    public Decimal Price { get; set; }
    [Range(1, 99)]
    public int Quantity { get; set; }

}