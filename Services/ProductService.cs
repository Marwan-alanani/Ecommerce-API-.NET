global using AutoMapper;
global using Domain.Contracts;
using Domain.Models;

namespace Services;

public class ProductService(IUnitOfWork unitOfWork,IMapper mapper): IProductService
{
    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
    {
        var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();
        return mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<ProductResponse> GetProductAsync(int id)
    {
        var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);
        return mapper.Map<ProductResponse>(product);
    }

    public async Task<IEnumerable<BrandResponse>> GetBrandsAsync()
    {
        // 1. Get Repository using unit of work GetRepository()
        var repo = unitOfWork.GetRepository<ProductBrand, int>();
        var brands = await repo.GetAllAsync();
        // 2. Use AutoMapper to convert from ProductType to TypeResponse
        return mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandResponse>>(brands);
    }

    public async Task<IEnumerable<TypeResponse>> GetTypesAsync()
    {
        // 1. Get Repository using unit of work GetRepository()
        var repo = unitOfWork.GetRepository<ProductType, int>();
        var types = await repo.GetAllAsync();
        // 2. Use AutoMapper to convert from ProductType to TypeResponse
        return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeResponse>>(types);
    }
}