namespace BooksAPI.Models.DTO.AuthorDto;

public class UpdateAuthorDto
{
    public string Name { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string UrlHandle { get; set; }
}