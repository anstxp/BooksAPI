using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Interface;

public interface IOrderRepository
{
    Task<Order> CreateOrderFromCartAsync(Guid userId, ShoppingCart cart);
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetOrdersByUserIdAsync(Guid userId);

}