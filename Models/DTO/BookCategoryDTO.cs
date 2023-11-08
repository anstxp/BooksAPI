namespace BooksAPI.Models.DTO;

public class BookCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UrlHandle { get; set; }
    public string? CategoryImageUrl { get; set; }
}