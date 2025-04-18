using System.Linq.Expressions;
using Domain.Models;

namespace Services.Specifications;

public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product>
{
    // Use ctor to get product by id
    public ProductWithBrandAndTypeSpecification(int id) : base (p => p.Id == id)
    {
        // Add includes
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }

    // Use this ctor to get all products
    public ProductWithBrandAndTypeSpecification() : base(null)
    {
        // Add includes
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }
}