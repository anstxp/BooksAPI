namespace BooksAPI.Models.DTO;

public class UpdateAuthorDto
{
    public string Name { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string UrlHandle { get; set; }
}