using System.Reflection.Metadata;
using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IBlogPostCategoriesRepository
{
    Task<BlogPostCategory> CreateAsync(BlogPostCategory blogPostCategory);
    Task<IEnumerable<BlogPostCategory>> GetAllAsync();
    Task<BlogPostCategory?> GetById(Guid id);
    Task<BlogPostCategory?> UpdateAsync(BlogPostCategory blogPostCategory);
    Task<BlogPostCategory?> DeleteAsync(Guid id);
}