namespace BooksAPI.Models.DTO;

public class CreateBookCategoryDto
{
    public string Name { get; set; }
    public string UrlHandle { get; set; }
    public string? CategoryImageUrl { get; set; } 
}