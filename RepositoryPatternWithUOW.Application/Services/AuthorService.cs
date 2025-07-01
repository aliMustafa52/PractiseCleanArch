using Mapster;
using RepositoryPatternWithUOW.Application.Abstractions;
using RepositoryPatternWithUOW.Application.Dtos.Authors;
using RepositoryPatternWithUOW.Application.ViewModels.Authors;
using RepositoryPatternWithUOW.Domain.Abstractions;
using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Domain.Errors;


namespace RepositoryPatternWithUOW.Application.Services
{
    public class AuthorService(IUnitOfWork unitOfWork) : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<AuthorResponse>> GetByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author is null)
                return Result.Failure<AuthorResponse>(AuthorErrors.AuthorNotFound);

            var authorDto = author.Adapt<AuthorResponse>();

            return Result.Success(authorDto);
        }

        public async Task<Result<AuthorResponse>> AddAsync(AddAuthorDto authorDto, CancellationToken cancellationToken = default)
        {
            var author = authorDto.Adapt<Author>();

            var addedAuthor = await _unitOfWork.Authors.AddAsync(author, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var addedAuthorDto = addedAuthor.Adapt<AuthorResponse>();

            return Result.Success(addedAuthorDto);
        }
    }
}
