using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IBookRepository
{
    Task<Book> CreateAsync(Book book);

    Task<IEnumerable<Book>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
        string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

    Task<Book?> GetById(Guid id);
    Task<Book?> UpdateAsync(Book book);
    Task<Book?> DeleteAsync(Guid id);
    Task<Book?> GetByUrl(string url);
    Task<IEnumerable<Book?>> SearchByTitle(string title);
}