
using Shared.DataTransferObjects.Products;

namespace ServicesAbstraction;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
    Task<ProductResponse> GetProductAsync(int id);
    Task<IEnumerable<BrandResponse>> GetBrandAsync();
    Task<IEnumerable<TypeResponse>> GetTypeAsync();
}