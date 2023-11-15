using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.AuthorDto;

public class AuthorDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? AuthorImageUrl { get; set; }
    [Required]
    public string UrlHandle { get; set; }
}