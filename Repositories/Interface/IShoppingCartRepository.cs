using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IShoppingCartRepository
{
    Task<ShoppingCart> CreateCartForUser(Guid userId);
    Task<ShoppingCart?> GetCartByUserId(Guid userId);
    Task<ShoppingCart?> UpdateCart(ShoppingCart cart);
    Task<ShoppingCart?> GetCartById(Guid id);
    Task<ShoppingCart?> DeleteCartByUserId(Guid userId);
    Task<decimal> GetTotalPriceForCart(Guid cartId);
    Task<bool> DoesCartExistForUser(Guid userId);
}