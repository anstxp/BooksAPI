using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class ShoppingCartItem
{
    public Guid Id { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }
    public decimal Price => Quantity * Book.Price;
    [ForeignKey("CartId")]
    public ShoppingCart ShoppingCart { get; set; }
}