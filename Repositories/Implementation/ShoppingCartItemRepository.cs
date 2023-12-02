using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class ShoppingCartItemRepository : IShoppingCartItemRepository
{
    private readonly AppDbContext _dbContext;
    public ShoppingCartItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ShoppingCartItem> CreateAsync(ShoppingCartItem item)
    {
        await _dbContext.ShoppingCartItems.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<ShoppingCartItem> UpdateAsync(ShoppingCartItem item)
    {
        _dbContext.ShoppingCartItems.Update(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<ShoppingCartItem?> DeleteAsync(Guid id)
    {
        var existingItem = await _dbContext.ShoppingCartItems.FindAsync(id);
        if (existingItem == null)
        {
            return null;
        }

        _dbContext.ShoppingCartItems.Remove(existingItem);
        await _dbContext.SaveChangesAsync();
        return existingItem;
    }

    public async Task<ShoppingCartItem?> GetById(Guid id)
    {
        return await _dbContext.ShoppingCartItems
            .Include(x => x.Book)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<IEnumerable<ShoppingCartItem?>> GetItemsByCartId(Guid cartId)
    {
        return await _dbContext.ShoppingCartItems
            .Where(item => item.ShoppingCart.Id == cartId)
            .Include(item => item.Book)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ShoppingCartItem>> DeleteAllItemsInCart(Guid cartId)
    {
        var cartItems = await _dbContext.ShoppingCartItems
            .Where(item => item.ShoppingCart.Id == cartId)
            .ToListAsync();

        _dbContext.ShoppingCartItems.RemoveRange(cartItems);
        await _dbContext.SaveChangesAsync();
        return cartItems;
    }
}