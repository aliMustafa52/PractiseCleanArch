using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Api.Extensions;
using RepositoryPatternWithUOW.Application.Abstractions;
using RepositoryPatternWithUOW.Application.Dtos;
using RepositoryPatternWithUOW.Application.ViewModels.Authors;

namespace RepositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(IAuthorService authorService) : ControllerBase
    {
        private readonly IAuthorService _authorService = authorService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _authorService.GetByIdAsync(id);

            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpPost("")]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorRequest request, CancellationToken cancellationToken)
        {
            var authorDto = request.Adapt<AuthorDto>();
            var result = await _authorService.AddAsync(authorDto, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
                : result.ToProblem();
        }
    }
}
