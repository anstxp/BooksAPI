using System.ComponentModel.DataAnnotations.Schema;
using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO;

public class CreateOrderItemDto
{
    public Guid Id { get; set; }
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public BookDto Book { get; set; }
    public int Quantity { get; set; }
}