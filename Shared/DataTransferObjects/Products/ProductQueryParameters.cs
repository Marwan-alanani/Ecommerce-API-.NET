namespace Shared.DataTransferObjects.Products;

public class ProductQueryParameters
{
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public ProductSortingOptions? Sorting { get; set; }
    public string? Search { get; set; }
}