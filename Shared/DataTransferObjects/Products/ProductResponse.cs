namespace Shared.DataTransferObjects.Products;

// C# 9
public record ProductResponse()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public string BrandName { get; set; }
    public string TypeName { get; set; }
}
// Reference Type
//  Equality Based on Value