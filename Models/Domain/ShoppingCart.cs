using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class ShoppingCart
{
    public Guid Id { get; set; }
    public ICollection<ShoppingCartItem> Items { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }
    public decimal Total
    {
        get
        {
            return Items.Sum(item => item.Quantity * item.Book.Price);
        }
    }
}