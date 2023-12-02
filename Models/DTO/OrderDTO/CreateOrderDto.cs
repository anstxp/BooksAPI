namespace BooksAPI.Models.DTO.OrderDTO;

public class CreateOrderDto
{
    public DateTime OrderDate { get; set; }
    public Guid User { get; set; } 
    public Guid[] Books { get; set; }
    public decimal Total { get; set; }
}