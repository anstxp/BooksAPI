using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO;

public class CreateBookCategoryDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string UrlHandle { get; set; }
    public string? CategoryImageUrl { get; set; } 
}