using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Domain.Interfaces;

namespace RepositoryPatternWithUOW.Application.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Author> Authors { get; }
        IBookRepository Books { get; }

        Task<int> SaveChangesAsync();
    }
}
