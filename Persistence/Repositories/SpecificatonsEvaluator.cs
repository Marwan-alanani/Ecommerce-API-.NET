namespace Persistence.Repositories;

public static class SpecificatonsEvaluator
{
    public static IQueryable<T> CreateQuery<T>(IQueryable<T> inputQuery, ISpecifications<T> specifications)
        where T : class
    {
        var query = inputQuery;
        if (specifications.Criteria is not null) query = query.Where(specifications.Criteria);
        // foreach (var include in specifications.IncludeExpressions)
        // {
        //    query.Include(include);
        // }
        query =
            specifications.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));
        return query;
    }
}