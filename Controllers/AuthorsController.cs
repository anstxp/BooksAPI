using System.IO.Compression;
using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.AuthorDto;
using BooksAPI.Models.DTO.BookDTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : Controller
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;
    
    public AuthorsController(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorDto request)
    {
        //DTO to Domain model
        var author = new Author
        {
            FullName = request.FullName,
            UrlHandle = request.UrlHandle, 
            Description = request.Description,
            AuthorImageUrl = request.AuthorImageUrl
        };

        await _authorRepository.CreateAsync(author);
        
        //Domain model to DTO
        var response = new AuthorDto
        {
            Id = author.Id,
            FullName = author.FullName,
            UrlHandle = author.UrlHandle,
            Description = author.Description,
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
                FullName = author.FullName,
                UrlHandle = author.UrlHandle,
                AuthorImageUrl = author.AuthorImageUrl,
                Description = author.Description,
                Books = author.Books.Select(x => new BookDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Description = x.Description!
                }).ToList()
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
            FullName = existingAuthor.FullName,
            UrlHandle = existingAuthor.UrlHandle,
            AuthorImageUrl = existingAuthor.AuthorImageUrl,
            Description = existingAuthor.Description,
            Books = existingAuthor.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description!
            }).ToList()
        };
        return Ok(response);
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetAuthorByName(string name)
    {
        var existingAuthor = await _authorRepository.GetByName(name);
        if (existingAuthor is null)
            return NotFound();
        var response = new AuthorDto
        {
            Id = existingAuthor.Id,
            FullName = existingAuthor.FullName,
            UrlHandle = existingAuthor.UrlHandle,
            AuthorImageUrl = existingAuthor.AuthorImageUrl,
            Description = existingAuthor.Description,
            Books = existingAuthor.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description!
            }).ToList()
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
            FullName = request.FullName,
            UrlHandle = request.UrlHandle,
            AuthorImageUrl = request.AuthorImageUrl,
            Description = request.Description,
            Books = new List<Book>()
        };
        
        foreach (var bookGuid in request.Books)
        {
            var existingBook = await _bookRepository.GetById(bookGuid);
            if (existingBook != null)
            {
                author.Books.Add(existingBook);
            }
        }
        
        author = await _authorRepository.UpdateAsync(author);
        
        if (author == null)
        {
            return NotFound();
        }

        var response = new AuthorDto()
        {
            Id = author.Id,
            FullName = author.FullName,
            UrlHandle = author.UrlHandle,
            AuthorImageUrl = author.AuthorImageUrl,
            Description = author.Description,
            Books = author.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description!
            }).ToList()
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
            FullName = author.FullName,
            UrlHandle = author.UrlHandle,
            AuthorImageUrl = author.AuthorImageUrl,
            Description = author.Description
        };

        return Ok(response);
    }
}