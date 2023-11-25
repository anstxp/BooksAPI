using System.Runtime.InteropServices.JavaScript;
using BooksAPI.Models.DTO.AuthDTO;
using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO.CommentDTO;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public UserDto User { get; set; }
    public BookDto Book { get; set; }
}