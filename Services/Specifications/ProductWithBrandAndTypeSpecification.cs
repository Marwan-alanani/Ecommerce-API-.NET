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
    public ProductWithBrandAndTypeSpecification(ProductQueryParameters parameters)
        : base(CreateCriteria(parameters))
    {
        // Add includes
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
        if (parameters.Sorting.HasValue) ApplySorting(parameters.Sorting.Value);
    }

    private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
    {
        return product =>
            (!parameters.BrandId.HasValue || parameters.BrandId.Value == product.BrandId) &&
            (!parameters.TypeId.HasValue || parameters.TypeId.Value == product.TypeId) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search));
    }

    private void ApplySorting(ProductSortingOptions options)
    {
        switch (options)
        {
            case ProductSortingOptions.NameAsc:
                AddOrderBy(p => p.Name);
                break;
            case ProductSortingOptions.NameDesc:
                AddOrderByDescending(p => p.Name);
                break;
            case ProductSortingOptions.PriceAsc:
                AddOrderBy(p => p.Price);
                break;
            case ProductSortingOptions.PriceDesc:
                AddOrderByDescending(p => p.Price);
                break;
            default:
                break;
        }
    }
}