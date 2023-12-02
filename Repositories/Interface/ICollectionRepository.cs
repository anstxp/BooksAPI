using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface ICollectionRepository
{ 
        Task<Collection> CreateAsync(Collection bookCategory); 
        Task<IEnumerable<Collection>> GetAllAsync();
        Task<Collection?> GetById(Guid id);
        Task<Collection?> GetByName(string name);
        Task<Collection?> UpdateAsync(Collection bookCategory);
        Task<Collection?> DeleteAsync(Guid id);
}