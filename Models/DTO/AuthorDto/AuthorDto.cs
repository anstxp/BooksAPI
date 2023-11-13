namespace BooksAPI.Models.DTO;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string UrlHandle { get; set; }
}