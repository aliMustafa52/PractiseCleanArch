using RepositoryPatternWithUOW.Application.Abstractions;
using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Domain.Interfaces;
using RepositoryPatternWithUOW.Infrastructure.Repositories;

namespace RepositoryPatternWithUOW.Infrastructure.Persistence
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;

        // Private fields for lazy initialization
        private IBaseRepository<Author>? _authors;
        private IBookRepository? _books;

        // Public properties with lazy initialization

        //public IBaseRepository<Author> Authors => _authors is null 
        //    ? new BaseRepository<Author>(_context) 
        //    : _authors;
        // eaul to null-coalescing assignment operator
        public IBaseRepository<Author> Authors => _authors ??= new BaseRepository<Author>(_context);
        public IBookRepository Books => _books ??= new BookRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
