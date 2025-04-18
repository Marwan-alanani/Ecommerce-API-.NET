using System.Linq.Expressions;
using Domain.Models;

namespace Services.Specifications;

public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product>
{
    // Use ctor to get product by id
    public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
    {
        // Add includes
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }

    // Use this ctor to get all products
    // Use for sorting & Filtration
    public ProductWithBrandAndTypeSpecification(int? brandId, int? typeId)
        : base(product =>
            (!brandId.HasValue || brandId.Value == product.BrandId) &&
            (!typeId.HasValue || typeId.Value == product.TypeId))
    {
        // Add includes
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }
}