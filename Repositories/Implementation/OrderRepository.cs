using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.OrderDTO;
using BooksAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories.Implementation;
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderFromCartAsync(Guid userId, ShoppingCart cart)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
        if (user == null)
        {
            // Handle user not found scenario
            return null;
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderDate = DateTime.UtcNow,
            User = user,
            OrderItems = cart.Items.Select(item => new OrderItem
            { 
                Book = item.Book,
                Quantity = item.Quantity
            }).ToList(),
            Total = cart.Items.Sum(item => item.Book.Price * item.Quantity)
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }
    
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.User)
            .ThenInclude(o => o.UserInfo)
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Book)
            .ToListAsync();
    }
    
    public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .Where(o => o.User.Id == userId.ToString())
            .ToListAsync();
    }
}