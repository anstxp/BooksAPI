using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IAuthorRepository
{
    Task<Author> CreateAsync(Author author);
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetById(Guid id);
    Task<Author?> UpdateAsync(Author author);
    Task<Author?> DeleteAsync(Guid id);
}