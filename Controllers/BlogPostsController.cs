using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogPostsController : Controller
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IBlogPostCategoriesRepository _blogPostCategoriesRepository;
    public BlogPostsController(IBookRepository bookRepository, 
        IBlogPostRepository blogPostRepository,
        IBlogPostCategoriesRepository blogPostCategoriesRepository)
    {
        _blogPostRepository = blogPostRepository;
        _bookRepository = bookRepository;
        _blogPostCategoriesRepository = blogPostCategoriesRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlogPost(CreateBlogPostDto request)
    {
        var blogPost = new BlogPost
        {
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            ImageUrl = request.ImageUrl,
            UrlHandle = request.UrlHandle,
            PublishDate = request.PublishDate,
            UserId = request.UserId,
            IsVisible = request.IsVisible,
            Categories = new List<BlogPostCategory>(),
            Books = new List<Book>()
        };

        foreach (var categoryGuid in request.Categories)
        {
            var existingCategory = await _blogPostCategoriesRepository.GetById(categoryGuid);
            if (existingCategory != null)
                blogPost.Categories.Add(existingCategory);
        }

        foreach (var bookGuid in request.Books)
        {
            var existingBook = await _bookRepository.GetById(bookGuid);
            if (existingBook != null)
                blogPost.Books.Add(existingBook);
        }

        blogPost = await _blogPostRepository.CreateAsync(blogPost);
        var response = new BlogPostDto
        {
            Id = blogPost.Id,
            Title = blogPost.Title,
            Description = blogPost.Description,
            Content = blogPost.Content,
            UrlHandle = blogPost.UrlHandle,
            PublishDate = blogPost.PublishDate,
            UserId = blogPost.UserId,
            IsVisible = blogPost.IsVisible,
            ImageUrl = blogPost.ImageUrl,
            Categories = blogPost.Categories.Select(x => new BlogPostCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,

            }).ToList(),
            Books = blogPost.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                UrlHadle = x.UrlHadle
            }).ToList()
        };
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBlogPosts([FromQuery] string? filterOn,
        [FromQuery] string? filterQuery, string? sortBy = null, bool isAscending = true,
        [FromQuery] int pageNumber = 1, [FromQuery]int pageSize = 1000)
    {
        var blogPosts = await _blogPostRepository.GetAllAsync(filterOn, filterQuery, 
            sortBy, isAscending, pageNumber, pageSize);
        
        //Convert Domain model to DTO
        var response = new List<BlogPostDto>();
        foreach(var blogPost in blogPosts)
        {
            response.Add(new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Description = blogPost.Description,
                Content = blogPost.Content,
                UrlHandle = blogPost.UrlHandle,
                PublishDate = blogPost.PublishDate,
                UserId = blogPost.UserId,
                IsVisible = blogPost.IsVisible,
                ImageUrl = blogPost.ImageUrl,
                Categories = blogPost.Categories.Select(x => new BlogPostCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,

                }).ToList(),
                Books = blogPost.Books.Select(x => new BookDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UrlHadle = x.UrlHadle
                }).ToList()
            });
        }
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
    {
        var existingBlogPost = await _blogPostRepository.GetById(id);
        if (existingBlogPost is null)
            return NotFound();
        var response = new BlogPostDto
        {
            Id = existingBlogPost.Id,
            Title = existingBlogPost.Title,
            Description = existingBlogPost.Description,
            Content = existingBlogPost.Content,
            UrlHandle = existingBlogPost.UrlHandle,
            PublishDate = existingBlogPost.PublishDate,
            UserId = existingBlogPost.UserId,
            IsVisible = existingBlogPost.IsVisible,
            ImageUrl = existingBlogPost.ImageUrl,
            Categories = existingBlogPost.Categories.Select(x => new BlogPostCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,

            }).ToList(),
            Books = existingBlogPost.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                UrlHadle = x.UrlHadle
            }).ToList()
        };
        return Ok(response);
    }
    
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> EditBook([FromRoute] Guid id, UpdateBlogPostDto request)
    {
        //DTO to Domain model
        var blogPost = new BlogPost
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            ImageUrl = request.ImageUrl,
            UrlHandle = request.UrlHandle,
            PublishDate = request.PublishDate,
            UserId = request.UserId,
            IsVisible = request.IsVisible,
            Categories = new List<BlogPostCategory>(),
            Books = new List<Book>()
        };

        foreach (var categoryGuid in request.Categories!)
        {
            var existingCategory = await _blogPostCategoriesRepository.GetById(categoryGuid);
            if (existingCategory != null)
            {
                blogPost.Categories.Add(existingCategory);
            }
        }
        
        foreach (var bookGuid in request.Books!)
        {
            var existingBook = await _bookRepository.GetById(bookGuid);
            if (existingBook != null)
            {
                blogPost.Books.Add(existingBook);
            }
        }
        
        var updatedBlogPost = await _blogPostRepository.UpdateAsync(blogPost);
        if (updatedBlogPost == null)
        {
            return NotFound();
        }
        // Convert Domain model to DTO
        var response = new BlogPostDto()
        {
            Id = blogPost.Id,
            Title = blogPost.Title,
            Description = blogPost.Description,
            Content = blogPost.Content,
            UrlHandle = blogPost.UrlHandle,
            PublishDate = blogPost.PublishDate,
            UserId = blogPost.UserId,
            IsVisible = blogPost.IsVisible,
            ImageUrl = blogPost.ImageUrl,
            Categories = blogPost.Categories.Select(x => new BlogPostCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle,

            }).ToList(),
            Books = blogPost.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                UrlHadle = x.UrlHadle
            }).ToList()
        };
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<ActionResult> DeleteBlogPost([FromRoute] Guid id)
    {
        var blogPost = await _blogPostRepository.DeleteAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        var response = new BlogPostDto
        {
            Id = blogPost.Id,
            Title = blogPost.Title,
            Description = blogPost.Description,
            Content = blogPost.Content,
            UrlHandle = blogPost.UrlHandle,
            PublishDate = blogPost.PublishDate,
            UserId = blogPost.UserId,
            IsVisible = blogPost.IsVisible,
            ImageUrl = blogPost.ImageUrl,
        };
        
        return Ok(response);
    }
    
    
}