using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace BooksAPI.Models.Domain;

public class ShoppingCartItem
{
    public Guid Id { get; set; }
    [ForeignKey("Book")]
    public Book Book { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
}