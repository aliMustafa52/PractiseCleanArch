using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Domain.Specifications;

namespace RepositoryPatternWithUOW.Infrastructure.Repositories
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery
            ,ISpecification<T> specification)
        {
            var query = inputQuery;

            // 1. Apply Criteria (WHERE clause)
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

                //foreach (var includeExpress in specification.Includes)
                //{
                //    query = query.Include(includeExpress);
                //}

                query = specification.Includes
                    .Aggregate(query, (current, include) => current.Include(include));

                //query.Include(include)
                //query.Include(include).Include(include)
                //query.Include(include).Include(include).Include(include)

            return query;
        }
    }
}
