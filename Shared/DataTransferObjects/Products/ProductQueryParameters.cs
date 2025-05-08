namespace Shared.DataTransferObjects.Products;

public class ProductQueryParameters
{
    private const int DefaultPageSize = 5;
    private const int MaxPageSize = 10;
    private int _pageSize = DefaultPageSize;
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public ProductSortingOptions? Sort { get; set; }
    public string? Search { get; set; }
    public int PageSize { get => _pageSize;
        set => _pageSize = (value > 0 &&  value <= MaxPageSize) ? value : DefaultPageSize; }
    public int PageIndex { get; set; } = 1;
}