using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly AppDbContext _dbContext;
    public BlogPostRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
        await _dbContext.BlogPosts.AddAsync(blogPost);
        await _dbContext.SaveChangesAsync();
        return blogPost;
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,
        int pageNumber = 1, int pageSize = 1000)
    {
        var blogPosts = _dbContext.BlogPosts.Include(x => x.Categories).
            Include(x => x.Books).AsQueryable();
        
        //Filtering
        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
            {
                blogPosts = blogPosts.Where(x => x.Categories.Any(c => c.Name.Contains(filterQuery)));
            }
            else if (filterOn.Equals("Book", StringComparison.OrdinalIgnoreCase))
            {
                blogPosts = blogPosts.Where(x => x.Books.Any(c => c.Title.Contains(filterQuery)));
            }
        }
        
        //Sorting
        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                blogPosts = isAscending ? blogPosts.OrderBy(x => x.Title) : blogPosts.OrderByDescending(x => x.Title);
            }
            if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
            {
                blogPosts = isAscending ? blogPosts.OrderBy(x => x.PublishDate) : blogPosts.OrderByDescending(x => x.PublishDate);
            }
        }
        
        //Pagination
        var skipResults = (pageNumber - 1) * pageSize;
        
        
        return await blogPosts.Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task<BlogPost?> GetById(Guid id)
    {
        return await _dbContext.BlogPosts.Include(x=>x.Categories).
            Include(x=>x.Books).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        var existingPost = await _dbContext.BlogPosts.Include(x=>x.Categories).
            Include(x=>x.Books).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
        if (existingPost != null)
        {
            _dbContext.Entry(existingPost).CurrentValues.SetValues(blogPost);
            existingPost.Categories = blogPost.Categories;
            existingPost.Books = blogPost.Books;
            await _dbContext.SaveChangesAsync();
            return blogPost;
        }
        return null;
    }

    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
        var existingPost = await _dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
        if (existingPost is null)
        {
            return null;
        }

        _dbContext.BlogPosts.Remove(existingPost);
        await _dbContext.SaveChangesAsync();
        return existingPost;
    }
}