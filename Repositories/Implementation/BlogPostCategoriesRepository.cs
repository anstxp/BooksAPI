using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class BlogPostCategoriesRepository : IBlogPostCategoriesRepository
{
    private readonly AppDbContext _dbContext;

    public BlogPostCategoriesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<BlogPostCategory> CreateAsync(BlogPostCategory blogPostCategory)
    {
        await _dbContext.BlogPostCategories.AddAsync(blogPostCategory);
        await _dbContext.SaveChangesAsync();

        return blogPostCategory;
    }

    public async Task<IEnumerable<BlogPostCategory>> GetAllAsync()
    {
        return await _dbContext.BlogPostCategories.ToListAsync();
    }

    public async Task<BlogPostCategory?> GetById(Guid id)
    {
        return await _dbContext.BlogPostCategories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPostCategory?> UpdateAsync(BlogPostCategory blogPostCategory)
    {
        var existingCategory = await _dbContext.BlogPostCategories.
            FirstOrDefaultAsync(x => x.Id == blogPostCategory.Id);
        if (existingCategory != null)
        {
            _dbContext.Entry(existingCategory).CurrentValues.SetValues(blogPostCategory);
            await _dbContext.SaveChangesAsync();
            return blogPostCategory;
        }

        return null;
    }

    public async Task<BlogPostCategory?> DeleteAsync(Guid id)
    {
        var existingCategory = await _dbContext.BlogPostCategories.FirstOrDefaultAsync(x => x.Id == id);
        if (existingCategory is null)
        {
            return null;
        }

        _dbContext.BlogPostCategories.Remove(existingCategory);
        await _dbContext.SaveChangesAsync();
        return existingCategory;
    }
}