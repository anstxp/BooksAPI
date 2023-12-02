using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User> CreateAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _dbContext.Users.Include(x=>x.UserInfo)
            .FirstOrDefaultAsync(x => x.Id == id.ToString());
    }

    public async Task<User?> GetByUserName(string userName)
    {
        return await _dbContext.Users.Include(x=>x.UserInfo)
            .FirstOrDefaultAsync(x => x.UserName!.ToLower() == userName.ToLower());
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var existingUser =  await _dbContext.Users.Include(x=>x.UserInfo)
            .FirstOrDefaultAsync(x => x.Id == user.Id);
        if (existingUser != null)
        {
            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
            existingUser.UserInfo = user.UserInfo;
            await _dbContext.SaveChangesAsync();
            return user;
        }
        return null;
    }

    public async Task<User?> DeleteAsync(Guid id)
    {
        var existingUser =  await _dbContext.Users.Include(x=>x.UserInfo)
            .FirstOrDefaultAsync(x => x.Id == id.ToString());
        if (existingUser is null)
        {
            return null;
        }
        
        var idInfo = existingUser.UserInfo.Id;
        var existingUserInfo = await _dbContext.UserInfos
            .FirstOrDefaultAsync(x => x.Id == idInfo);
        
        _dbContext.UserInfos.Remove(existingUserInfo!);
        _dbContext.Users.Remove(existingUser);
        await _dbContext.SaveChangesAsync();
        return existingUser;
    }

    public async Task<UserInfo?> DeleteInfoAsync(Guid id)
    {
        var user = await _dbContext.Users.Include(x=>x.UserInfo)
            .FirstOrDefaultAsync(x => x.Id == id.ToString());
        var idInfo = user!.UserInfo.Id;
        var existingUserInfo =  await _dbContext.UserInfos
            .FirstOrDefaultAsync(x => x.Id == idInfo);
        _dbContext.UserInfos.Remove(existingUserInfo!);
        await _dbContext.SaveChangesAsync();
        return existingUserInfo;
    }
}