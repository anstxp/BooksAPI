
using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.BookCategoryDto;
using BooksAPI.Models.DTO.BookDTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookCategoriesController : Controller
{
    private readonly IBookCategoryRepository _bookCategoryRepository;
    private readonly ILogger<BookCategoriesController> _logger;
    private readonly IBookRepository _bookRepository;

    public BookCategoriesController(IBookCategoryRepository bookCategoryRepository,
        ILogger<BookCategoriesController> logger, IBookRepository bookRepository)
    {
        _bookCategoryRepository = bookCategoryRepository;
        _logger = logger;
        _bookRepository = bookRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBookCategory(CreateBookCategoryDto request)
    {
        //DTO to Domain model
        var bookCategory = new BookCategory
        {
            Name = request.Name,
            UrlHandle = request.UrlHandle,
            Description = request.Description
        };

        await _bookCategoryRepository.CreateAsync(bookCategory);
        
        //Domain model to DTO
        var response = new BookCategoryDto
        {
            Id = bookCategory.Id,
            Name = bookCategory.Name,
            UrlHandle = bookCategory.UrlHandle,
            Description = bookCategory.Description,
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookCategories()
    {
        _logger.LogInformation("GetAllBookCategories Action Method was invoked");
        
        var bookCategories = await _bookCategoryRepository.GetAllAsync();
        //Domain model to DTO
        var response = new List<BookCategoryDto>();
        foreach (var bookCategory in bookCategories)
        {
            response.Add(new BookCategoryDto
            {
                Id = bookCategory.Id,
                Name = bookCategory.Name,
                UrlHandle = bookCategory.UrlHandle,
                Description = bookCategory.Description!,
                Books = bookCategory.Books.Select(x => new BookDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Description = x.Description!,
                    ImageUrl = x.ImageUrl,
                    UrlHadle = x.UrlHandle
                }).ToList()
            });
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetBookCategoryById([FromRoute] Guid id)
    {
        var existingCategory = await _bookCategoryRepository.GetById(id);
        if (existingCategory is null)
            return NotFound();
        var response = new BookCategoryDto
        {
            Id = existingCategory.Id,
            Name = existingCategory.Name,
            UrlHandle = existingCategory.UrlHandle,
            Description = existingCategory.Description!,
            Books = existingCategory.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description!,
                ImageUrl = x.ImageUrl,
                UrlHadle = x.UrlHandle
            }).ToList()
        };
        return Ok(response);
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetBookCategoryByName(string name)
    {
        var existingCategory = await _bookCategoryRepository.GetByName(name);
        if (existingCategory is null)
            return NotFound();
        var response = new BookCategoryDto
        {
            Id = existingCategory.Id,
            Name = existingCategory.Name,
            UrlHandle = existingCategory.UrlHandle,
            Description = existingCategory.Description!,
            Books = existingCategory.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description!,
                ImageUrl = x.ImageUrl,
                UrlHadle = x.UrlHandle
            }).ToList()
        };
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> EditBookCategory([FromRoute] Guid id, UpdateBookCategoryDto request)
    {
        //DTO to Domain model
        var bookCategory = new BookCategory
        {
            Id = id,
            Name = request.Name,
            UrlHandle = request.UrlHandle,
            Description = request.Description,
            Books = new List<Book>()
        };
        
        foreach (var bookGuid in request.Books)
        {
            var existingBook = await _bookRepository.GetById(bookGuid);
            if (existingBook != null)
            {
                bookCategory.Books.Add(existingBook);
            }
        }
        
        bookCategory = await _bookCategoryRepository.UpdateAsync(bookCategory);
        
        if (bookCategory == null)
        {
            return NotFound();
        }

        var response = new BookCategoryDto
        {
            Id = bookCategory.Id,
            Name = bookCategory.Name,
            UrlHandle = bookCategory.UrlHandle,
            Description = bookCategory.Description!,
            Books = bookCategory.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description!,
                ImageUrl = x.ImageUrl,
                UrlHadle = x.UrlHandle
            }).ToList()
        };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<ActionResult> DeleteBookCategory([FromRoute] Guid id)
    {
        var bookCategory = await _bookCategoryRepository.DeleteAsync(id);
        if (bookCategory == null)
        {
            return NotFound();
        }

        var response = new BookCategoryDto
        {
            Id = bookCategory.Id,
            Name = bookCategory.Name,
            UrlHandle = bookCategory.UrlHandle,
            Description = bookCategory.Description!,
        };

        return Ok(response);
    }

}