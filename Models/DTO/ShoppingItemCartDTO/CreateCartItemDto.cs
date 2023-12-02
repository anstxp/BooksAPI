using BooksAPI.Models.Domain;

namespace BooksAPI.Models.DTO.ShoppingItemCartDTO;

public class CreateCartItemDto
{
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
}