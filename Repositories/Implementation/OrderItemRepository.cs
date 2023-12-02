using BooksAPI.Data;
using BooksAPI.Models.Domain;

namespace BooksAPI.Repositories.Implementation;

public class OrderItemRepository
{
    private readonly AppDbContext _context;

    public OrderItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public OrderItem AddOrderItem(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        _context.SaveChanges();
        return orderItem;
    }

    public List<OrderItem> GetOrderItemsByOrderId(Guid orderId)
    {
        return _context.OrderItems
            .Where(oi => oi.Id == orderId)
            .ToList();
    }

    public OrderItem RemoveOrderItem(Guid orderItemId)
    {
        var orderItem = _context.OrderItems.Find(orderItemId);
        if (orderItem != null)
        {
            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();
            return orderItem;
        }

        return null;
    }
}