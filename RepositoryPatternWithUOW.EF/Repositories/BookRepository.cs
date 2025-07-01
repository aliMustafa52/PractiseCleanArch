using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Domain.Interfaces;
using RepositoryPatternWithUOW.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Infrastructure.Repositories
{
    public class BookRepository(ApplicationDbContext context) 
        : BaseRepository<Book>(context), IBookRepository
    {
        public async Task<IEnumerable<Book>> GetBooksByAuthorNameAsync(string authorName, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(b => b.Author)
                .Where(b => b.Author.Name == authorName)
                .ToListAsync(cancellationToken);
        }
    }
}
