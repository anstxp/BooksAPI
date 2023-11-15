using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO;
using BooksAPI.Models.DTO.BlogPostCategoryDto;
using BooksAPI.Models.DTO.BookCategoryDto;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogPostCategoriesController : Controller
{
    private readonly IBlogPostCategoriesRepository _blogPostCategories;
    
    public BlogPostCategoriesController(IBlogPostCategoriesRepository blogPostCategoriesRepository)
    {
        _blogPostCategories = blogPostCategoriesRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBlogPostCategory(CreateBlogPostCategoryDto request)
    {
        //DTO to Domain model
        var blogPostCategory = new BlogPostCategory
        {
            Name = request.Name,
            UrlHandle = request.UrlHandle,
        };

        await _blogPostCategories.CreateAsync(blogPostCategory);
        
        //Domain model to DTO
        var response = new BookCategoryDto
        {
            Id = blogPostCategory.Id,
            Name = blogPostCategory.Name,
            UrlHandle = blogPostCategory.UrlHandle,
        };
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBlogPostCategories()
    {
        var blogPostCategories = await _blogPostCategories.GetAllAsync();
        //Domain model to DTO
        var response = new List<BlogPostCategoryDto>();
        foreach (var blogPostCategory in blogPostCategories)
        {
            response.Add(new BlogPostCategoryDto
            {
                Id = blogPostCategory.Id,
                Name = blogPostCategory.Name,
                UrlHandle = blogPostCategory.UrlHandle,
            });
        }

        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetBlogPostCategoryById([FromRoute] Guid id)
    {
        var existingCategory = await _blogPostCategories.GetById(id);
        if (existingCategory is null)
            return NotFound();
        var response = new BlogPostCategoryDto
        {
            Id = existingCategory.Id,
            Name = existingCategory.Name,
            UrlHandle = existingCategory.UrlHandle,
        };
        return Ok(response);
    }
    
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> EditBlogPostCategory([FromRoute] Guid id, UpdateBlogPostCategoryDto request)
    {
        //DTO to Domain model
        var blogPostCategory = new BlogPostCategory
        {
            Id = id,
            Name = request.Name,
            UrlHandle = request.UrlHandle,
        };
        blogPostCategory = await _blogPostCategories.UpdateAsync(blogPostCategory);
        
        if (blogPostCategory == null)
        {
            return NotFound();
        }

        var response = new BlogPostCategoryDto
        {
            Id = blogPostCategory.Id,
            Name = blogPostCategory.Name,
            UrlHandle = blogPostCategory.UrlHandle,
        };
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<ActionResult> DeleteBlogPostCategory([FromRoute] Guid id)
    {
        var blogPostCategory = await _blogPostCategories.DeleteAsync(id);
        if (blogPostCategory == null)
        {
            return NotFound();
        }

        var response = new BookCategoryDto
        {
            Id = blogPostCategory.Id,
            Name = blogPostCategory.Name,
            UrlHandle = blogPostCategory.UrlHandle,
        };
        return Ok(response);
    }

}