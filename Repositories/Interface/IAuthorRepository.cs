using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IAuthorRepository
{
    Task<Author> CreateAsync(Author author);
    Task<IEnumerable<Author>> GetAllAsync(string? sortBy = null, bool isAscending = true, 
        int pageNumber = 1, int pageSize = 1000);
    Task<Author?> GetById(Guid id);
    Task<Author?> GetByName(string name);
    Task<Author?> UpdateAsync(Author author);
    Task<Author?> DeleteAsync(Guid id);
    
}