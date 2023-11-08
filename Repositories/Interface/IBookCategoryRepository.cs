using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IBookCategoryRepository
{
    Task<BookCategory> CreateAsync(BookCategory bookCategory);
    Task<IEnumerable<BookCategory>> GetAllAsync();
    Task<BookCategory?> GetById(Guid id);
    Task<BookCategory?> UpdateAsync(BookCategory bookCategory);
    Task<BookCategory?> DeleteAsync(Guid id);
}