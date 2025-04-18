
using Shared.DataTransferObjects.Products;

namespace ServicesAbstraction;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId, ProductSortingOptions options);
    Task<ProductResponse> GetProductAsync(int id);
    Task<IEnumerable<BrandResponse>> GetBrandsAsync();
    Task<IEnumerable<TypeResponse>> GetTypesAsync();
}