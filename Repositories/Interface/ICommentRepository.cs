using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface ICommentRepository 
{
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> DeleteAsync(Guid id);
    Task<IEnumerable<Comment?>> GetCommentsAsync();
}