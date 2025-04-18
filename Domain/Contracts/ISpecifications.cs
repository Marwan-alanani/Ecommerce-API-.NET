using System.Linq.Expressions;

namespace Domain.Contracts;

public interface ISpecifications<T> where T : class
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> IncludeExpressions { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    Expression<Func<T, object>> OrderBy { get; }
}
// Where => Criteria Expression<Func<T,bool>>
// Include => List<Expression<Func<T,object>>>
// Select Criteria Expression<Func<T,TResult>>
// Ordering ... etc