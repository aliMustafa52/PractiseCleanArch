using RepositoryPatternWithUOW.Domain.Abstractions;
using RepositoryPatternWithUOW.Domain.Errors;

namespace RepositoryPatternWithUOW.Api.Mappings
{
    public static class ErrorCodeToHttpStatusMapper
    {
        private static readonly Dictionary<Error, int> ErrorStatusCodeMap = new()
        {
            { AuthorErrors.AuthorNotFound, StatusCodes.Status404NotFound },
            { AuthorErrors.DuplicatedAuthorName, StatusCodes.Status409Conflict }
        };

        public static int Map(Error error)
        {
            // Try to get specific status code, fallback to 400 Bad Request if not found
            if (!ErrorStatusCodeMap.TryGetValue(error, out int statusCode))
            {
                // Default for unmapped errors
                statusCode = StatusCodes.Status400BadRequest;
            }

            return statusCode;
        }
    }
}
