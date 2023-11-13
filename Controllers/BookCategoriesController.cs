using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookCategoriesController : Controller
{
    private readonly IBookCategoryRepository _bookCategoryRepository;
    
    public BookCategoriesController(IBookCategoryRepository bookCategoryRepository)
    {
        _bookCategoryRepository = bookCategoryRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBookCategory(CreateBookCategoryDto request)
    {
        //DTO to Domain model
        var bookCategory = new BookCategory
        {
            Name = request.Name,
            UrlHandle = request.UrlHandle,
            CategoryImageUrl = request.CategoryImageUrl
        };

        await _bookCategoryRepository.CreateAsync(bookCategory);
        
        //Domain model to DTO
        var response = new BookCategoryDto
        {
            Id = bookCategory.Id,
            Name = bookCategory.Name,
            UrlHandle = bookCategory.UrlHandle,
            CategoryImageUrl = bookCategory.CategoryImageUrl
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookCategories()
    {
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
                CategoryImageUrl = bookCategory.CategoryImageUrl
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
            CategoryImageUrl = existingCategory.CategoryImageUrl
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
            CategoryImageUrl = request.CategoryImageUrl
        };
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
            CategoryImageUrl = bookCategory.CategoryImageUrl
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
            CategoryImageUrl = bookCategory.CategoryImageUrl
        };

        return Ok(response);
    }

}