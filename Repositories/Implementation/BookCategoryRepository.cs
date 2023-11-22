using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class BookCategoryRepository : IBookCategoryRepository
{
    private readonly AppDbContext _dbContext;

    public BookCategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookCategory> CreateAsync(BookCategory bookCategory)
    {
        await _dbContext.BookCategories.AddAsync(bookCategory);
        await _dbContext.SaveChangesAsync();

        return bookCategory;
    }

    public async Task<IEnumerable<BookCategory>> GetAllAsync()
    {
        return await _dbContext.BookCategories.ToListAsync();
    }

    public async Task<BookCategory?> GetById(Guid id)
    {
        return await _dbContext.BookCategories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BookCategory?> GetByName(string name)
    {
        return await _dbContext.BookCategories.
            FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
    }

    public async Task<BookCategory?> UpdateAsync(BookCategory bookCategory)
    {
        var existingCategory = await _dbContext.BookCategories
            .FirstOrDefaultAsync(x => x.Id == bookCategory.Id);
        
        if (existingCategory == null) return null;
        
        _dbContext.Entry(existingCategory).CurrentValues.SetValues(bookCategory);
        await _dbContext.SaveChangesAsync();
        return bookCategory;

    }
    
    public async Task<BookCategory?> DeleteAsync(Guid id)
    {
        var existingCategory = await _dbContext.BookCategories.FirstOrDefaultAsync(x => x.Id == id);
        
        if (existingCategory is null) return null;

        _dbContext.BookCategories.Remove(existingCategory);
        await _dbContext.SaveChangesAsync();
        return existingCategory;
    }
}
