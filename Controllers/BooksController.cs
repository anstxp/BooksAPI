using System.Collections.ObjectModel;
using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO;
using BooksAPI.Models.DTO.AuthDTO;
using BooksAPI.Models.DTO.AuthorDto;
using BooksAPI.Models.DTO.BookCategoryDto;
using BooksAPI.Models.DTO.BookDTO;
using BooksAPI.Models.DTO.CollectionDTO;
using BooksAPI.Models.DTO.CommentDTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookCategoryRepository _categoryRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICollectionRepository _collectionRepository;
    public BooksController(IBookRepository bookRepository, 
        IBookCategoryRepository categoryRepository,
        IAuthorRepository authorRepository, ICollectionRepository collectionRepository)
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
        _authorRepository = authorRepository;
        _collectionRepository = collectionRepository;
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto request)
    {
        var book = new Book
        {
            Title = request.Title,
            Description = request.Description,
            ISBN = request.ISBN,
            PageCount = request.PageCount,
            ImageUrl = request.ImageUrl,
            UrlHandle = request.UrlHadle, 
            Price = request.Price,
            Categories = new List<BookCategory>(),
            Collections = new List<Collection>(),
            Authors = new List<Author>(),
            Comments = new List<Comment>(),
        };

        foreach (var categoryGuid in request.Categories)
        {
            var existingCategory = await _categoryRepository.GetById(categoryGuid);
            if(existingCategory !=null )
                book.Categories.Add(existingCategory);
        }
        
        foreach (var authorGuid in request.Authors)
        {
            var existingAuthor = await _authorRepository.GetById(authorGuid);
            if(existingAuthor !=null )
                book.Authors.Add(existingAuthor);
        }
        
        foreach (var collectionGuid in request.Collections)
        {
            var existingCollection = await _collectionRepository.GetById(collectionGuid);
            if(existingCollection !=null )
                book.Collections.Add(existingCollection);
        }

        book = await _bookRepository.CreateAsync(book);
        var response = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            PageCount = book.PageCount,
            ImageUrl = book.ImageUrl,
            UrlHadle = book.UrlHandle, 
            Price = book.Price,
            Categories = book.Categories.Select(x => new BookCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,
                Description = x.Description!
            }).ToList(),
            Collection = book.Collections.Select(x => new CollectionDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UrlHandle = x.UrlHandle
            }).ToList(),
            Authors = book.Authors.Select(x => new AuthorDto
            {
                Id = x.Id,
                FullName = x.FullName,
                UrlHandle = x.UrlHandle,
                AuthorImageUrl = x.AuthorImageUrl,
                Description = x.Description
            }).ToList(),
            Comments = book.Comments.Select(x => new CommentDto
            {
                Id = x.Id,
                Content = x.Content,
                PublishDate = x.Date,
                User = new UserDto()
                {
                    Id = x.User.Id,
                    UserName = x.User.UserName!
                }
            }).ToList()
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery] string? filterOn,
        [FromQuery] string? filterQuery, string? sortBy = null, bool isAscending = true,
        [FromQuery] int pageNumber = 1, [FromQuery]int pageSize = 1000)
    {
        var books = await _bookRepository.GetAllAsync(filterOn, filterQuery, 
            sortBy, isAscending, pageNumber, pageSize);
        
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
                ImageUrl = book.ImageUrl,
                UrlHadle = book.UrlHandle, 
                Price = book.Price,
                Categories = book.Categories.Select(x => new BookCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                    Description = x.Description!
                }).ToList(),
                Collection = book.Collections.Select(x => new CollectionDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UrlHandle = x.UrlHandle
                }).ToList(),
                Authors = book.Authors.Select(x => new AuthorDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    UrlHandle = x.UrlHandle,
                    AuthorImageUrl = x.AuthorImageUrl,
                    Description = x.Description
                }).ToList(),
                Comments = book.Comments.Select(x => new CommentDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    User = new UserDto()
                    {
                        Id = x.User.Id,
                        UserName = x.User.UserName!,
                        Email = x.User.Email!,
                    }
                }).ToList()
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
            ImageUrl = existingBook.ImageUrl,
            UrlHadle = existingBook.UrlHandle, 
            Price = existingBook.Price,
            Categories = existingBook.Categories.Select(x => new BookCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,
                Description = x.Description!
            }).ToList(),
            Collection = existingBook.Collections.Select(x => new CollectionDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UrlHandle = x.UrlHandle
            }).ToList(),
            Authors = existingBook.Authors.Select(x => new AuthorDto
            {
                Id = x.Id,
                FullName = x.FullName,
                UrlHandle = x.UrlHandle,
                AuthorImageUrl = x.AuthorImageUrl,
                Description = x.Description
            }).ToList(),
            Comments = existingBook.Comments.Select(x => new CommentDto
            {
                Id = x.Id,
                Content = x.Content,
                User = new UserDto()
                {
                    Id = x.User.Id,
                    UserName = x.User.UserName!,
                    Email = x.User.Email!,
                }
            }).ToList()

        };
        return Ok(response);
    }
    
    [Authorize(Roles = "admin")]
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
            ImageUrl = request.ImageUrl,
            UrlHandle = request.UrlHadle, 
            Price = request.Price,
            Categories = new List<BookCategory>(),
            Authors = new List<Author>(),
            Collections = new List<Collection>(),
        };

        foreach (var categoryGuid in request.Categories)
        {
            var existingCategory = await _categoryRepository.GetById(categoryGuid);
            if (existingCategory != null)
            {
                book.Categories.Add(existingCategory);
            }
        }
        
        foreach (var authorGuid in request.Authors)
        {
            var existingAuthor = await _authorRepository.GetById(authorGuid);
            if (existingAuthor != null)
            {
                book.Authors.Add(existingAuthor);
            }
        }
        
        foreach (var collectionGuid in request.Collections)
        {
            var existingCollection = await _collectionRepository.GetById(collectionGuid);
            if (existingCollection != null)
            {
                book.Collections.Add(existingCollection);
            }
        }
        
        var updatedBook = await _bookRepository.UpdateAsync(book);
        if (updatedBook == null)
        {
            return NotFound();
        }
        // Convert Domain model to DTO
        var response = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            PageCount = book.PageCount,
            ImageUrl = book.ImageUrl,
            UrlHadle = book.UrlHandle, 
            Price = book.Price,
            Categories = book.Categories.Select(x => new BookCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,
                Description = x.Description!
            }).ToList(),
            Collection = book.Collections.Select(x => new CollectionDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UrlHandle = x.UrlHandle
            }).ToList(),
            Authors = book.Authors.Select(x => new AuthorDto
            {
                Id = x.Id,
                FullName = x.FullName,
                UrlHandle = x.UrlHandle,
                AuthorImageUrl = x.AuthorImageUrl,
                Description = x.Description
            }).ToList(),
        };
        return Ok(response);
    }
    
    [Authorize]
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
            ImageUrl = book.ImageUrl,
            UrlHadle = book.UrlHandle, 
            Price = book.Price,
        };
        
        return Ok(response);
    }
    
    // [HttpGet("{title}")]
    // public async Task<IActionResult> GetBookByName(string title)
    // {
    //     var existingBook = await _bookRepository.GetByTitle(title);
    //     if (existingBook is null)
    //         return NotFound();
    //     var response = new BookDto
    //     {
    //         Id = existingBook.Id,
    //         Title = existingBook.Title,
    //         Description = existingBook.Description,
    //         ISBN = existingBook.ISBN,
    //         PageCount = existingBook.PageCount,
    //         ImageUrl = existingBook.ImageUrl,
    //         UrlHadle = existingBook.UrlHandle, 
    //         Price = existingBook.Price,
    //         Categories = existingBook.Categories.Select(x => new BookCategoryDto
    //         {
    //             Id = x.Id,
    //             Name = x.Name,
    //             UrlHandle = x.UrlHandle,
    //             Description = x.Description!
    //         }).ToList(),
    //         Collection = existingBook.Collections.Select(x => new CollectionDto
    //         {
    //             Id = x.Id,
    //             Name = x.Name,
    //             Description = x.Description,
    //             UrlHandle = x.UrlHandle
    //         }).ToList(),
    //         Authors = existingBook.Authors.Select(x => new AuthorDto
    //         {
    //             Id = x.Id,
    //             FullName = x.FullName,
    //             UrlHandle = x.UrlHandle,
    //             AuthorImageUrl = x.AuthorImageUrl,
    //             Description = x.Description
    //         }).ToList(),
    //         Comments = existingBook.Comments.Select(x => new CommentDto
    //         {
    //             Id = x.Id,
    //             Content = x.Content,
    //             User = new UserDto()
    //             {
    //                 Id = x.User.Id,
    //                 UserName = x.User.UserName!,
    //                 Email = x.User.Email!,
    //             }
    //         }).ToList()
    //         
    //     };
    //     return Ok(response);
    // }
    //
    
    [HttpGet("{urlHandle}")]
    public async Task<IActionResult> GetBookByUrl(string urlHandle)
    {
        var existingBook = await _bookRepository.GetByUrl(urlHandle);
        if (existingBook is null)
            return NotFound();
        var response = new BookDto
        {
            Id = existingBook.Id,
            Title = existingBook.Title,
            Description = existingBook.Description,
            ISBN = existingBook.ISBN,
            PageCount = existingBook.PageCount,
            ImageUrl = existingBook.ImageUrl,
            UrlHadle = existingBook.UrlHandle, 
            Price = existingBook.Price,
            Categories = existingBook.Categories.Select(x => new BookCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,
                Description = x.Description!
            }).ToList(),
            Collection = existingBook.Collections.Select(x => new CollectionDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UrlHandle = x.UrlHandle
            }).ToList(),
            Authors = existingBook.Authors.Select(x => new AuthorDto
            {
                Id = x.Id,
                FullName = x.FullName,
                UrlHandle = x.UrlHandle,
                AuthorImageUrl = x.AuthorImageUrl,
                Description = x.Description
            }).ToList(),
            Comments = existingBook.Comments.Select(x => new CommentDto
            {
                Id = x.Id,
                Content = x.Content,
                User = new UserDto()
                {
                    Id = x.User.Id,
                    UserName = x.User.UserName!,
                    Email = x.User.Email!,
                }
            }).ToList()

        };
        return Ok(response);
    }
    
    [HttpGet("/search/{query}")]
    public async Task<IActionResult> SearchByQuery(string query)
    {
        var existingBooks = await _bookRepository.SearchByTitle(query);
        var response = new List<BookDto>();
        foreach(var book in existingBooks)
        {
            response.Add(new BookDto
            {
                Id = book!.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                PageCount = book.PageCount,
                ImageUrl = book.ImageUrl,
                UrlHadle = book.UrlHandle, 
                Price = book.Price,
                Categories = book.Categories.Select(x => new BookCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                    Description = x.Description!
                }).ToList(),
                Collection = book.Collections.Select(x => new CollectionDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UrlHandle = x.UrlHandle
                }).ToList(),
                Authors = book.Authors.Select(x => new AuthorDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    UrlHandle = x.UrlHandle,
                    AuthorImageUrl = x.AuthorImageUrl,
                    Description = x.Description
                }).ToList(),
                Comments = book.Comments.Select(x => new CommentDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    User = new UserDto()
                    {
                        Id = x.User.Id,
                        UserName = x.User.UserName!,
                        Email = x.User.Email!,
                    }
                }).ToList()
            });
        }
        return Ok(response);
    }
}