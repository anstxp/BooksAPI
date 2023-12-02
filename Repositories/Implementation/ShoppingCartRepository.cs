using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly AppDbContext _dbContext;

    public ShoppingCartRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ShoppingCart?> CreateCartForUser(Guid userId)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId.ToString());
        var existingCart = await _dbContext.ShoppingCarts
            .FirstOrDefaultAsync(cart => cart!.User.Id == userId.ToString());

        if (existingUser == null) return null;
        if (existingCart != null) return existingCart;

        var newCart = new ShoppingCart
        {
            User = existingUser,
            Items = new List<ShoppingCartItem>(),
        };

        _dbContext.ShoppingCarts.Add(newCart);
        await _dbContext.SaveChangesAsync();

        return newCart;
    }

    public async Task<ShoppingCart?> GetCartByUserId(Guid userId)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId.ToString());
        if (existingUser == null) return null;
        var existingCart = await _dbContext.ShoppingCarts
            .Include(cart => cart!.Items)
            .ThenInclude(item => item.Book)
            .FirstOrDefaultAsync(cart => cart!.User.Id == userId.ToString());
        return existingCart;
    }
    
    public async Task<ShoppingCart?> GetCartById(Guid id)
    {
        var existingCart = await _dbContext.ShoppingCarts
            .Include(x => x.Items)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
        return existingCart;
    }

    public async Task<ShoppingCart> UpdateCart(ShoppingCart cart)
    {
        var existingCart = await _dbContext.ShoppingCarts
            .Include(x => x.Items)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == cart.Id);

        if (existingCart == null) return null;

        _dbContext.Entry(existingCart).CurrentValues.SetValues(cart);

        existingCart.Items = cart.Items;

        await _dbContext.SaveChangesAsync();
        return cart;
    }

    public async Task<ShoppingCart?> DeleteCartByUserId(Guid userId)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId.ToString());
        if (existingUser == null) return null;
        var existingCart = await _dbContext.ShoppingCarts
            .Include(cart => cart!.Items)
            .FirstOrDefaultAsync(cart => cart!.User.Id == userId.ToString());

        if (existingCart == null) return null;

        _dbContext.ShoppingCarts.Remove(existingCart);
        await _dbContext.SaveChangesAsync();
        return existingCart;
    }

    public async Task<decimal> GetTotalPriceForCart(Guid cartId)
    {
        var existingCart = await _dbContext.ShoppingCarts
            .Include(cart => cart!.Items)
            .FirstOrDefaultAsync(cart => cart!.Id == cartId);
        if (existingCart == null) return 0;

        return existingCart.Total;
    }

    public async Task<bool> DoesCartExistForUser(Guid userId)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId.ToString());
        if (existingUser == null) return false;
        var existingCart = await _dbContext.ShoppingCarts
            .FirstOrDefaultAsync(cart => cart!.User.Id == userId.ToString());
        if (existingCart == null) return false;
        return true;
    }
}