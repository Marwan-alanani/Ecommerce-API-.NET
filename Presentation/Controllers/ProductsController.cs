global using ServicesAbstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Products;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IServiceManager serviceManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProductResponse>> GetAllProducts(
        int? brandId
        , int? typeId, ProductSortingOptions
            sorting) // Get BaseUrl/api/Products
    {
        var products =
            await serviceManager.ProductService.GetAllProductsAsync(brandId, typeId,sorting);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProductBy(int id) // Get BaseUrl/api/{id:int}
    {
        var product = await serviceManager.ProductService.GetProductAsync(id);
        return Ok(product);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands()
    {
        var brands = await serviceManager.ProductService.GetBrandsAsync();
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes()
    {
        var types = await serviceManager.ProductService.GetTypesAsync();
        return Ok(types);
    }
}