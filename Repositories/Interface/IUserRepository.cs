using BooksAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Repositories.Interface;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User?> GetById(Guid id);
    Task<User?> GetByUserName(string userName);
    Task<User?> UpdateAsync(User user);
    Task<User?> DeleteAsync(Guid id);
    Task<UserInfo?> DeleteInfoAsync(Guid id);
}