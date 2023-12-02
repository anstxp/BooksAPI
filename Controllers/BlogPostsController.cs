using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.AuthDTO;
using BooksAPI.Models.DTO.AuthorDto;
using BooksAPI.Models.DTO.BlogPostDto;
using BooksAPI.Models.DTO.BookCategoryDto;
using BooksAPI.Models.DTO.BookDTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogPostsController : Controller
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IUserRepository _userRepository;

    public BlogPostsController(IBookRepository bookRepository, 
        IBlogPostRepository blogPostRepository,
        UserManager<IdentityUser> userManager,
        IUserRepository userRepository, IAuthorRepository authorRepository)
    {
        _blogPostRepository = blogPostRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateBlogPost(CreateBlogPostDto request)
    {
        var existingUser = await _userRepository.GetById(request.User);
        if (existingUser != null)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                Book = request.Book,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                PublishDate = request.PublishDate,
                IsVisible = request.IsVisible,
                User = existingUser,
            };

            blogPost = await _blogPostRepository.CreateAsync(blogPost);
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Book = request.Book,
                Content = blogPost.Content,
                PublishDate = blogPost.PublishDate,
                User = new UserDto()
                {
                    Id = blogPost.User.Id,
                    UserName = blogPost.User.UserName!,
                    Email = blogPost.User.Email!,
                    UserInfo = new UserInfoDto()
                    {
                        FirstName = blogPost.User.UserInfo.FirstName,
                        LastName = blogPost.User.UserInfo.LastName,
                        ProfilePhotoUrl = blogPost.User.UserInfo.ProfilePhotoUrl
                    }
                },
                IsVisible = blogPost.IsVisible,
                ImageUrl = blogPost.ImageUrl,
            };
            return Ok(response);
        }
        
        return BadRequest();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBlogPosts([FromQuery] string? filterOn,
        [FromQuery] string? filterQuery, string? sortBy = null, bool isAscending = true,
        [FromQuery] int pageNumber = 1, [FromQuery]int pageSize = 1000)
    {
        var blogPosts = await _blogPostRepository.GetAllAsync(filterOn, filterQuery, 
            sortBy, isAscending, pageNumber, pageSize);
        
        var response = new List<BlogPostDto>();
        foreach(var blogPost in blogPosts)
        {
            response.Add(new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Book = blogPost.Book,
                Content = blogPost.Content,
                PublishDate = blogPost.PublishDate,
                IsVisible = blogPost.IsVisible,
                ImageUrl = blogPost.ImageUrl,
                User = new UserDto()
                {
                    Id = blogPost.User.Id,
                    UserName = blogPost.User.UserName!,
                    Email = blogPost.User.Email!,
                    UserInfo = new UserInfoDto()
                    {
                        FirstName = blogPost.User.UserInfo.FirstName,
                        LastName = blogPost.User.UserInfo.LastName,
                        ProfilePhotoUrl = blogPost.User.UserInfo.ProfilePhotoUrl
                    }
                },
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
            Book = existingBlogPost.Book,
            Content = existingBlogPost.Content,
            PublishDate = existingBlogPost.PublishDate,
            User = new UserDto()
            {
                Id = existingBlogPost.User.Id,
                UserName = existingBlogPost.User.UserName!,
                Email = existingBlogPost.User.Email!,
                UserInfo = new UserInfoDto()
                {
                    FirstName = existingBlogPost.User.UserInfo.FirstName,
                    LastName = existingBlogPost.User.UserInfo.LastName,
                    ProfilePhotoUrl = existingBlogPost.User.UserInfo.ProfilePhotoUrl
                }
            },
            IsVisible = existingBlogPost.IsVisible,
            ImageUrl = existingBlogPost.ImageUrl,
        };
        return Ok(response);
    }
    
    [Authorize]
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> EditBook([FromRoute] Guid id, UpdateBlogPostDto request)
    {
        //DTO to Domain model
        var blogPost = new BlogPost
        {
            Id = id,
            Title = request.Title,
            Book = request.Book,
            Content = request.Content,
            ImageUrl = request.ImageUrl,
            PublishDate = request.PublishDate,
            IsVisible = request.IsVisible,
        };
        
        var updatedBlogPost = await _blogPostRepository.UpdateAsync(blogPost);
        if (updatedBlogPost == null)
        {
            return NotFound();
        }

        var response = new BlogPostDto()
        {
            Id = updatedBlogPost.Id,
            Title = updatedBlogPost.Title,
            Book = updatedBlogPost.Book,
            Content = updatedBlogPost.Content,
            PublishDate = updatedBlogPost.PublishDate,
            IsVisible = updatedBlogPost.IsVisible,
            ImageUrl = updatedBlogPost.ImageUrl
        };
        return Ok(response);
    }
    
    [Authorize]
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
            Book = blogPost.Book,
            Content = blogPost.Content,
            PublishDate = blogPost.PublishDate,
            IsVisible = blogPost.IsVisible,
            ImageUrl = blogPost.ImageUrl,
        };
        
        return Ok(response);
    }
    
    
}