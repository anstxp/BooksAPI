using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class CollectionRepository : ICollectionRepository
{
    private readonly AppDbContext _dbContext;

    public CollectionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Collection> CreateAsync(Collection collection)
    {
        await _dbContext.Collections.AddAsync(collection);
        await _dbContext.SaveChangesAsync();

        return collection;
    }

    public async Task<IEnumerable<Collection>> GetAllAsync()
    {
        return await _dbContext.Collections
            .Include(x => x.Books)
            .ToListAsync();
    }

    public async Task<Collection?> GetById(Guid id)
    {
        return await _dbContext.Collections
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Collection?> GetByName(string name)
    {
        return await _dbContext.Collections
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
    }

    public async Task<Collection?> UpdateAsync(Collection collection)
    {
        var existingCollection = await _dbContext.Collections
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == collection.Id);
        
        if (existingCollection == null) return null;
        
        _dbContext.Entry(existingCollection).CurrentValues.SetValues(collection);
        await _dbContext.SaveChangesAsync();
        return collection;
    }

    public async Task<Collection?> DeleteAsync(Guid id)
    {
        var existingCollection = await _dbContext.Collections.FirstOrDefaultAsync(x => x.Id == id);
        
        if (existingCollection is null) return null;

        _dbContext.Collections.Remove(existingCollection);
        await _dbContext.SaveChangesAsync();
        return existingCollection;
    }
}