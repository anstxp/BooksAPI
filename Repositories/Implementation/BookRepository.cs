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

    public async Task<IEnumerable<Book>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
        string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
    {
        var books = _dbContext.Books.
            Include(x => x.Categories)
            .Include(x => x.Authors)
            .Include(x => x.Comments)
            .ThenInclude(comment => comment.User)
            .AsQueryable();
        
        //Filtering
        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
            {
                books = books.Where(x => x.Categories.Any(c => c.Name.Contains(filterQuery)));
            }
            else if (filterOn.Equals("Author", StringComparison.OrdinalIgnoreCase))
            {
                books = books.Where(x => x.Authors.Any(c => c.FullName.Contains(filterQuery)));
            }
        }
        
        //Sorting
        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                books = isAscending ? books.OrderBy(x => x.Title) : books.OrderByDescending(x => x.Title);
            }
        }
        
        //Pagination
        var skipResults = (pageNumber - 1) * pageSize;
        
        return await books.Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task<Book?> GetById(Guid id)
    {
        return await _dbContext.Books.Include(x=>x.Categories).
            Include(x=>x.Authors)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Book?> GetByTitle(string title)
    {
        return await _dbContext.Books.Include(x=>x.Categories)
            .Include(x=>x.Authors)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Title.ToLower() == title.ToLower());
    }

    public async Task<Book?> UpdateAsync(Book book)
    {
        var existingBook = await _dbContext.Books.Include(x=>x.Categories).
            Include(x=>x.Authors)
            .FirstOrDefaultAsync(x => x.Id == book.Id);
        
        if (existingBook == null) return null;
        
        _dbContext.Entry(existingBook).CurrentValues.SetValues(book);
        
        existingBook.Categories = book.Categories;
        existingBook.Authors = book.Authors;
        
        await _dbContext.SaveChangesAsync();
        return book;
    }
    
    public async Task<Book?> DeleteAsync(Guid id)
    {
        var existingBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (existingBook is null) return null;

        _dbContext.Books.Remove(existingBook);
        await _dbContext.SaveChangesAsync();
        return existingBook;
    }
}