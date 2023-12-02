using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class ShoppingCart
{
    public Guid Id { get; set; }
    public ICollection<ShoppingCartItem> Items { get; set; }
    public decimal Total => Items?.Sum(item => item.Price * item.Quantity) ?? 0;
    public User User { get; set; }
}