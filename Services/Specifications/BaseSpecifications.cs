using System.Linq.Expressions;

namespace Services.Specifications;

public abstract class BaseSpecifications<T> : ISpecifications<T> where T : class
{
    public Expression<Func<T, bool>> Criteria { get; private set; }
    public List<Expression<Func<T, object>>> IncludeExpressions { get; } = [];

    public BaseSpecifications(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Expression<Func<T, object>> include) =>
        IncludeExpressions.Add(include);
}