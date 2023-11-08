using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _dbContext;
    public BookRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Book> CreateAsync(Book book)
    {
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
        return book;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }

    public async Task<Book?> GetById(Guid id)
    {
        return await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Book?> UpdateAsync(Book book)
    {
        var existingBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
        if (existingBook != null)
        {
            _dbContext.Entry(existingBook).CurrentValues.SetValues(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        return null;
    }
    
    public async Task<Book?> DeleteAsync(Guid id)
    {
        var existingBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (existingBook is null)
        {
            return null;
        }

        _dbContext.Books.Remove(existingBook);
        await _dbContext.SaveChangesAsync();
        return existingBook;
    }
}