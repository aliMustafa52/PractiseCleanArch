using RepositoryPatternWithUOW.Application.Dtos.Authors;
using RepositoryPatternWithUOW.Application.ViewModels.Authors;
using RepositoryPatternWithUOW.Domain.Abstractions;

namespace RepositoryPatternWithUOW.Application.Abstractions
{
    public interface IAuthorService
    {
        Task<Result<AuthorResponse>> GetByIdAsync(int id);

        Task<Result<AuthorResponse>> AddAsync(AddAuthorDto authorDto, CancellationToken cancellationToken = default);
    }
}
