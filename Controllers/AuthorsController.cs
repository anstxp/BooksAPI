using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : Controller
{
    private readonly IAuthorRepository _authorRepository;
    
    public AuthorsController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorDto request)
    {
        //DTO to Domain model
        var author = new Author
        {
            Name = request.Name,
            UrlHandle = request.UrlHandle, 
            AuthorImageUrl = request.AuthorImageUrl
        };

        await _authorRepository.CreateAsync(author);
        
        //Domain model to DTO
        var response = new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            UrlHandle = author.UrlHandle,
            AuthorImageUrl = author.AuthorImageUrl
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _authorRepository.GetAllAsync();
        //Domain model to DTO
        var response = new List<AuthorDto>();
        foreach (var author in authors)
        {
            response.Add(new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                UrlHandle = author.UrlHandle,
                AuthorImageUrl = author.AuthorImageUrl
            });
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetAuthorById([FromRoute] Guid id)
    {
        var existingAuthor = await _authorRepository.GetById(id);
        if (existingAuthor is null)
            return NotFound();
        var response = new AuthorDto
        {
            Id = existingAuthor.Id,
            Name = existingAuthor.Name,
            UrlHandle = existingAuthor.UrlHandle,
            AuthorImageUrl = existingAuthor.AuthorImageUrl
        };
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> EditAuthor([FromRoute] Guid id, UpdateAuthorDto request)
    {
        //DTO to Domain model
        var author = new Author
        {
            Id = id,
            Name = request.Name,
            UrlHandle = request.UrlHandle,
            AuthorImageUrl = request.AuthorImageUrl
        };
        author = await _authorRepository.UpdateAsync(author);
        
        if (author == null)
        {
            return NotFound();
        }

        var response = new Author
        {
            Id = author.Id,
            Name = author.Name,
            UrlHandle = author.UrlHandle,
            AuthorImageUrl = author.AuthorImageUrl
        };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<ActionResult> DeleteAuthor([FromRoute] Guid id)
    {
        var author = await _authorRepository.DeleteAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        var response = new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            UrlHandle = author.UrlHandle,
            AuthorImageUrl = author.AuthorImageUrl
        };

        return Ok(response);
    }
}