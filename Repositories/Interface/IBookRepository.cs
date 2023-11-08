using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IBookRepository
{ 
    Task<Book> CreateAsync(Book book);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetById(Guid id);
    Task<Book?> UpdateAsync(Book book);
    Task<Book?> DeleteAsync(Guid id);
}