using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly AppDbContext _dbContext;

    public FeedbackRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Feedback> CreateAsync(Feedback feedback)
    {
        await _dbContext.Feedbacks.AddAsync(feedback);
        await _dbContext.SaveChangesAsync();

        return feedback;
    }

    public async Task<IEnumerable<Feedback>> GetAllAsync()
    {
        return await _dbContext.Feedbacks
            .OrderByDescending(x => x.Date)
            .ToListAsync();
    }
}