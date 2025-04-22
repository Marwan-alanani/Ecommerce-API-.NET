global using AutoMapper;
global using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Products;
using Services.Specifications;
using Shared;

namespace Services;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters queryParameters)
    {
        var specifications =
            new ProductWithBrandAndTypeSpecification(queryParameters); // Object that holds query parameters
        var products = await unitOfWork.GetRepository<Product, int>()
            .GetAllAsync(specifications);

        var data = mapper.Map<IEnumerable<ProductResponse>>(products);
        var pageIndex = queryParameters.PageIndex;
        var pageCount = data.Count();
        var totalCount = await unitOfWork.GetRepository<Product, int>()
            .CountAsync(new ProductCountSpecifications(queryParameters));

        return new PaginatedResponse<ProductResponse>(
            queryParameters.PageIndex,
            pageCount,
            totalCount,
            data
        );
    }


    public async Task<ProductResponse> GetProductAsync(int id)
    {
        var specifications = new ProductWithBrandAndTypeSpecification(id);
        var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specifications) ??
                      throw new ProductNotFoundException(id);
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