using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class OrderItem
{
    public Guid Id { get; set; }
    [ForeignKey("OrderId")]
    public Guid OrderId { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }
}