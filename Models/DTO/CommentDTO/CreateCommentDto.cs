
using BooksAPI.Models.DTO.AuthDTO;
using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO.CommentDTO;

public class CreateCommentDto
{
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public Guid User { get; set; }
    public Guid Book { get; set; }
}