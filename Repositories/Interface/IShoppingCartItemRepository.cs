using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IShoppingCartItemRepository
{
    Task<ShoppingCartItem> CreateAsync(ShoppingCartItem item);
    Task<ShoppingCartItem> UpdateAsync(ShoppingCartItem item);
    Task<ShoppingCartItem?> DeleteAsync(Guid id);
    Task<ShoppingCartItem?> GetById(Guid id);
    Task<IEnumerable<ShoppingCartItem?>> GetItemsByCartId(Guid cartId);
    Task<IEnumerable<ShoppingCartItem>> DeleteAllItemsInCart(Guid cartId);
}