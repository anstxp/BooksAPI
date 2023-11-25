using System.ComponentModel.DataAnnotations;
using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO.AuthorDto;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string? Description { get; set; }
    public string UrlHandle { get; set; }
    public List<BookDto>? Books { get; set; } = new();
}