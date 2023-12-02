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

    public async Task<IEnumerable<Author>> GetAllAsync(string? sortBy = null, bool isAscending = true, 
        int pageNumber = 1, int pageSize = 1000)
    {
        var authors = _dbContext.Authors
            .Include(x => x.Books)
            .OrderBy(x => x.FullName)
            .AsQueryable();
        
        //Sorting
        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                authors = isAscending ? authors.OrderBy(x => x.FullName) : 
                    authors.OrderByDescending(x => x.FullName);
            }
        }
        
        var skipResults = (pageNumber - 1) * pageSize;
        
        return await authors.Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task<Author?> GetById(Guid id)
    {
        return await _dbContext.Authors
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Author?> GetByName(string name)
    {
        return await _dbContext.Authors
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.FullName.ToLower() == name.ToLower());
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