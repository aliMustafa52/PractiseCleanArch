using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Domain.Interfaces;
using RepositoryPatternWithUOW.Domain.Specifications;
using RepositoryPatternWithUOW.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace RepositoryPatternWithUOW.Infrastructure.Repositories
{
    public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .SingleOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<TResult?> SingleAndProjectAsync<TResult>(
            Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector
            , CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .Select(selector)
                .SingleOrDefaultAsync(cancellationToken);
        }

        //public async Task<TResult?> SingleAndProjectWithSpecificationAsync<TResult>(
        //    Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector
        //    , Expression<Func<T, object>> includes, CancellationToken cancellationToken = default)
        //{
        //    return await _dbSet
        //        .Include(includes)
        //        .Where(predicate)
        //        .Select(selector)
        //        .SingleOrDefaultAsync(cancellationToken);
        //}
        public async Task<IEnumerable<T>> GetWithSpecificationAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvalutor<T>.GetQuery(_dbSet, spec);

            return await query.ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<TResult>> FindAndProjectAsync<TResult>(
            Expression<Func<T, bool>> predicate,Expression<Func<T, TResult>> selector
            ,CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);

            return entities;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
