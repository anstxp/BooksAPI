using System.ComponentModel.DataAnnotations;
using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO.BookCategoryDto;

public class BookCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UrlHandle { get; set; }
    public List<BookDto>? Books { get; set; } = new();
}