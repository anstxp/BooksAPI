using BooksAPI.Models.DTO.AuthDTO;

namespace BooksAPI.Models.DTO;

public class OrderDto
{
    public DateTime OrderDate { get; set; }
    public UserDto User { get; set; } 
    public IList<OrderItemDto> OrderItems { get; set; }
    public decimal Total { get; set; }
}