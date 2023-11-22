using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.AuthorDto;

public class CreateAuthorDto
{
    public string FullName { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string? Description { get; set; }
    public string UrlHandle { get; set; }
}