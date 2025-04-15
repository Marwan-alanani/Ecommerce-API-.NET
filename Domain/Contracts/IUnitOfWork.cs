using Domain.Models;

namespace Domain.Contracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();

    // IGenericRepository<Product,int > Products { get; }
    // IGenericRepository<ProductType ,int > ProductTypes { get; }
    // IGenericRepository<ProductBrand ,int > ProductBrands { get; }
    IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TEntity : BaseEntity<TKey>;
}