using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO;

public class CreateAuthorDto
{
    [Required]
    public string Name { get; set; }
    public string? AuthorImageUrl { get; set; }
    [Required]
    public string UrlHandle { get; set; }
}