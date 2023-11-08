using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _dbContext;
    public AuthorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Author> CreateAsync(Author author)
    {
        await _dbContext.Authors.AddAsync(author);
        await _dbContext.SaveChangesAsync();

        return author;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _dbContext.Authors.ToListAsync();
    }

    public async Task<Author?> GetById(Guid id)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Author?> UpdateAsync(Author author)
    {
        var existingAuthor = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == author.Id);
        if (existingAuthor != null)
        {
            _dbContext.Entry(existingAuthor).CurrentValues.SetValues(author);
            await _dbContext.SaveChangesAsync();
            return author;
        }

        return null;
    }
    
    public async Task<Author?> DeleteAsync(Guid id)
    {
        var existingAuthor = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (existingAuthor is null)
        {
            return null;
        }

        _dbContext.Authors.Remove(existingAuthor);
        await _dbContext.SaveChangesAsync();
        return existingAuthor;
    }
}