namespace Domain.Models.Products;

public class Product : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public ProductBrand ProductBrand { get; set; } // Reference Navigational Property
    public int BrandId { get; set; } // FK
    public ProductType ProductType { get; set; } // Reference Navigational Property
    public int TypeId { get; set; } // FK
}