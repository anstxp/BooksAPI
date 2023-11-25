using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.AuthorDto;

public class UpdateAuthorDto
{
    public string FullName { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string? Description { get; set; }
    public string UrlHandle { get; set; }
    public List<Guid> Books { get; set; } = new List<Guid>();
}