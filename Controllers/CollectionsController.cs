using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.BookCategoryDto;
using BooksAPI.Models.DTO.BookDTO;
using BooksAPI.Models.DTO.CollectionDTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CollectionsController : Controller
{
    private readonly ICollectionRepository _collectionRepository;
    private readonly IBookRepository _bookRepository;

    public CollectionsController(IBookRepository bookRepository, 
        ICollectionRepository collectionRepository)
    {
        _bookRepository = bookRepository;
        _collectionRepository = collectionRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCollection(CreateCollectionDto request)
    {
        //DTO to Domain model
        var collection = new Collection
        {
            Name = request.Name,
            Description = request.Description,
            UrlHandle = request.UrlHandle
        };

        await _collectionRepository.CreateAsync(collection);
        
        //Domain model to DTO
        var response = new BookCategoryDto
        {
            Id = collection.Id,
            Name = collection.Name,
            Description = collection.Description,
            UrlHandle = request.UrlHandle
        };
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCollections()
    {
        var collections = await _collectionRepository.GetAllAsync();
        //Domain model to DTO
        var response = new List<CollectionDto>();
        foreach (var collection in collections)
        {
            response.Add(new CollectionDto
            {
                Id = collection.Id,
                Name = collection.Name,
                Description = collection.Description,
                UrlHandle = collection.UrlHandle,
                Books = collection.Books.Select(x => new BookDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Description = x.Description!,
                    UrlHadle = x.UrlHandle,
                    ImageUrl = x.ImageUrl
                }).ToList()
            });
        }
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetCollectionById([FromRoute] Guid id)
    {
        var existingCollection = await _collectionRepository.GetById(id);
        if (existingCollection is null)
            return NotFound();
        var response = new BookCategoryDto
        {
            Id = existingCollection.Id,
            Name = existingCollection.Name,
            Description = existingCollection.Description,
            Books = existingCollection.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description!,
                UrlHadle = x.UrlHandle,
                ImageUrl = x.ImageUrl
            }).ToList()
        };
        return Ok(response);
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetBookCategoryByName(string name)
    {
        var existingCollection = await _collectionRepository.GetByName(name);
        if (existingCollection is null)
            return NotFound();
        var response = new BookCategoryDto
        {
            Id = existingCollection.Id,
            Name = existingCollection.Name,
            Description = existingCollection.Description,
            Books = existingCollection.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description!,
                UrlHadle = x.UrlHandle,
                ImageUrl = x.ImageUrl
            }).ToList()
        };
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<ActionResult> DeleteCollection([FromRoute] Guid id)
    {
        var collection = await _collectionRepository.DeleteAsync(id);
        if (collection == null)
        {
            return NotFound();
        }

        var response = new BookCategoryDto
        {
            Id = collection.Id,
            Name = collection.Name,
            Description = collection.Description!,
            Books = collection.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description!
            }).ToList()
        };

        return Ok(response);
    }

}