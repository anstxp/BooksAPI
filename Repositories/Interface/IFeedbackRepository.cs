using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IFeedbackRepository
{
    Task<Feedback> CreateAsync(Feedback feedback);
    Task<IEnumerable<Feedback>> GetAllAsync();
}