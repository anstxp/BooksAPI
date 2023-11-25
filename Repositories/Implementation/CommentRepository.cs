using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _dbContext;
    public CommentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }
    
    public async Task<Comment?> DeleteAsync(Guid id)
    {
        var existingComment = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if (existingComment is null) return null;

        _dbContext.Comments.Remove(existingComment);
        await _dbContext.SaveChangesAsync();
        return existingComment;
    }

    public async Task<IEnumerable<Comment?>> GetCommentsAsync()
    {
        var recentComments = _dbContext.Comments
            .Include(x => x.User)
            .ThenInclude(x => x.UserInfo)
            .Include(x => x.Book)
            .ThenInclude(x => x.Authors)
            .OrderByDescending(x => x.Date)
            .Take(3);

        return await recentComments.ToListAsync();
    }
}