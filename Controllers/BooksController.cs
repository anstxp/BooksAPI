using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : Controller
{
    private readonly IBookRepository _bookRepository;
    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto request)
    {
        var book = new Book
        {
            Title = request.Title,
            Description = request.Description,
            ISBN = request.ISBN,
            PageCount = request.PageCount,
            Publisher = request.Publisher,
            PublishedDate = request.PublishedDate,
            ImageUrl = request.ImageUrl,
            WebReaderLink = request.WebReaderLink,
            UrlHadle = request.UrlHadle, 
            Price = request.Price
        };

        book = await _bookRepository.CreateAsync(book);
        var response = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            PageCount = book.PageCount,
            Publisher = book.Publisher,
            PublishedDate = book.PublishedDate,
            ImageUrl = book.ImageUrl,
            WebReaderLink = book.WebReaderLink,
            UrlHadle = book.UrlHadle, 
            Price = book.Price
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookRepository.GetAllAsync();
        //Convert Domain model to DTO
        var response = new List<BookDto>();
        foreach(var book in books)
        {
            response.Add(new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                PageCount = book.PageCount,
                Publisher = book.Publisher,
                PublishedDate = book.PublishedDate,
                ImageUrl = book.ImageUrl,
                WebReaderLink = book.WebReaderLink,
                UrlHadle = book.UrlHadle, 
                Price = book.Price
            });
        }
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetBookById([FromRoute] Guid id)
    {
        var existingBook = await _bookRepository.GetById(id);
        if (existingBook is null)
            return NotFound();
        var response = new BookDto
        {
            Id = existingBook.Id,
            Title = existingBook.Title,
            Description = existingBook.Description,
            ISBN = existingBook.ISBN,
            PageCount = existingBook.PageCount,
            Publisher = existingBook.Publisher,
            PublishedDate = existingBook.PublishedDate,
            ImageUrl = existingBook.ImageUrl,
            WebReaderLink = existingBook.WebReaderLink,
            UrlHadle = existingBook.UrlHadle, 
            Price = existingBook.Price
        };
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> EditBook([FromRoute] Guid id, UpdateBookDto request)
    {
        //DTO to Domain model
        var book = new Book
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
            ISBN = request.ISBN,
            PageCount = request.PageCount,
            Publisher = request.Publisher,
            PublishedDate = request.PublishedDate,
            ImageUrl = request.ImageUrl,
            WebReaderLink = request.WebReaderLink,
            UrlHadle = request.UrlHadle, 
            Price = request.Price
        };
        var updatedBook = await _bookRepository.UpdateAsync(book);
        if (updatedBook == null)
        {
            return NotFound();
        }
        // Convert Domain model to DTO
        var response = new Book
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            PageCount = book.PageCount,
            Publisher = book.Publisher,
            PublishedDate = book.PublishedDate,
            ImageUrl = book.ImageUrl,
            WebReaderLink = book.WebReaderLink,
            UrlHadle = book.UrlHadle, 
            Price = book.Price
        };
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<ActionResult> DeleteBook([FromRoute] Guid id)
    {
        var book = await _bookRepository.DeleteAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        var response = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            PageCount = book.PageCount,
            Publisher = book.Publisher,
            PublishedDate = book.PublishedDate,
            ImageUrl = book.ImageUrl,
            WebReaderLink = book.WebReaderLink,
            UrlHadle = book.UrlHadle, 
            Price = book.Price
        };

        return Ok(response);
    }
    
}