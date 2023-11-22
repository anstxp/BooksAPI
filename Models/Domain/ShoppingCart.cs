using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class ShoppingCart
{
    public Guid Id { get; set; }
    [ForeignKey("Item")]
    [Required]
    public ICollection<ShoppingCartItem> Items { get; set; }
    [ForeignKey("User")]
    [Required]
    public User User { get; set; }
    public decimal Total
    {
        get
        {
            return Items.Sum(item => item.Quantity * item.Book.Price);
        }
    }
}