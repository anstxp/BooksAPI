
using BooksAPI.Models.DTO.AuthDTO;
using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO.BlogPostDto;

public class BlogPostDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    
    public DateTime PublishDate { get; set; }
    public UserDto? User { get; set; }
    public bool IsVisible { get; set; }
    public List<BookDto>? Books { get; set; } = new();
}
