using RepositoryPatternWithUOW.Domain.Abstractions;

namespace RepositoryPatternWithUOW.Domain.Errors
{
    public static class AuthorErrors
    {
        public static readonly Error AuthorNotFound =
            new("Author.NotFound", "No Author was found with the given ID");

        public static readonly Error DuplicatedAuthorName =
            new("Author.Duplicatedname", "Another Author with the same name is already exists");
    }
}
