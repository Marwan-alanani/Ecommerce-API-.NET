using System.Linq.Expressions;
using Domain.Models;
using Domain.Models.Products;

namespace Services.Specifications;

public class ProductCountSpecifications(ProductQueryParameters parameters)
    : BaseSpecifications<Product>(CreateCriteria(parameters))
{
    private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
    {
        return product =>
            (!parameters.BrandId.HasValue || parameters.BrandId.Value == product.BrandId) &&
            (!parameters.TypeId.HasValue || parameters.TypeId.Value == product.TypeId) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search));
    }
}