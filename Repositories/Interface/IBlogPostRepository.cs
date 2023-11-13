using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IBlogPostRepository
{
    Task<BlogPost> CreateAsync(BlogPost blogPost);
    Task<IEnumerable<BlogPost>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
        string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
    Task<BlogPost?> GetById(Guid id);
    Task<BlogPost?> UpdateAsync(BlogPost book);
    Task<BlogPost?> DeleteAsync(Guid id);
}