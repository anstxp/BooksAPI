using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class OrderItem
{
    public Guid Id { get; set; }
    [Required]
    [ForeignKey("Book")]
    public Book Book { get; set; }
    [Required]
    public int Quantity { get; set; }
}