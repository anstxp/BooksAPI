using System.ComponentModel.DataAnnotations.Schema;
using BooksAPI.Models.DTO.AuthDTO;
using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO.ShoppingItemCartDTO;

public class ShoppingCartItemDto
{
    public Guid Id { get; set; }
    public BookDto Book { get; set; }
    public UserDto User { get; set; }

    public int Quantity { get; set; } = 1;
    public decimal TotaL { get; set;  }
}