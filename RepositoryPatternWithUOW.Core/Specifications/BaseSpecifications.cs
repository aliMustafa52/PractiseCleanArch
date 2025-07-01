using RepositoryPatternWithUOW.Domain.Entities;
using System.Linq.Expressions;

namespace RepositoryPatternWithUOW.Domain.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = [];

        public BaseSpecifications()
        {
            //Includes = [];
        }

        public BaseSpecifications(Expression<Func<T, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
            //Includes = [];
        }
    }
}
