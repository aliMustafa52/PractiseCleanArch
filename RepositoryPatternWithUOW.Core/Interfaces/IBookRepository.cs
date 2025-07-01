using RepositoryPatternWithUOW.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Domain.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByAuthorNameAsync(string authorName, CancellationToken cancellationToken = default);
    }
}
