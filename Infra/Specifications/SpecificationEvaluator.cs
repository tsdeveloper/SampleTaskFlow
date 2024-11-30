using System.Diagnostics.CodeAnalysis;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Specifications
{
      [ExcludeFromCodeCoverage]

    public class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null) query = query.Where(spec.Criteria);

            if (spec.Orderby != null) query = query.OrderBy(spec.Orderby);

            if (spec.OrderByDescending != null)  query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPagingEnabled) query = query.Skip(spec.Skip).Take(spec.Take);
        
            if (spec.Includes != null && spec.Includes.Any())
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (spec.IncludeStrings != null && spec.IncludeStrings.Any())
            query = spec.IncludeStrings.Aggregate(query,
                                                (current, include) => current.Include(include));

            return query;
        }
    }
}
