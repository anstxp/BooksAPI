using System.ComponentModel.DataAnnotations;
using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO.BookCategoryDto;

public class BookCategoryDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string UrlHandle { get; set; }
    public string? CategoryImageUrl { get; set; }
    public ICollection<BookDto> Books { get; set; }
}