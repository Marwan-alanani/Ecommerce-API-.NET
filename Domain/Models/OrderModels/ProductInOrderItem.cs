namespace Domain.Models.OrderModels;

public class ProductInOrderItem
{
    public ProductInOrderItem(int productId, string productName, string pictureUrl)
    {
        ProductId = productId;
        ProductName = productName;
        PictureUrl = pictureUrl;
    }

    public ProductInOrderItem()
    {
        
    }
    
    public int ProductId { get; set; } // => Product id
    public string ProductName { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
}