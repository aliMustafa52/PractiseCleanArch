using Mapster;
using RepositoryPatternWithUOW.Application.Abstractions;
using RepositoryPatternWithUOW.Application.Dtos;
using RepositoryPatternWithUOW.Application.ViewModels.Authors;
using RepositoryPatternWithUOW.Domain.Abstractions;
using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Domain.Errors;
using RepositoryPatternWithUOW.Domain.Interfaces;


namespace RepositoryPatternWithUOW.Application.Services
{
    public class AuthorService(IBaseRepository<Author> authorRepository) : IAuthorService
    {
        private readonly IBaseRepository<Author> _authorRepository = authorRepository;

        public async Task<Result<AuthorResponse>> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author is null)
                return Result.Failure<AuthorResponse>(AuthorErrors.AuthorNotFound);

            var authorDto = author.Adapt<AuthorResponse>();

            return Result.Success(authorDto);
        }

        public async Task<Result<AuthorResponse>> AddAsync(AddAuthorDto authorDto, CancellationToken cancellationToken = default)
        {
            var author = authorDto.Adapt<Author>();
            var addedAuthor = await _authorRepository.AddAsync(author, cancellationToken);
            var addedAuthorDto = addedAuthor.Adapt<AuthorResponse>();

            return Result.Success(addedAuthorDto);
        }
    }
}
