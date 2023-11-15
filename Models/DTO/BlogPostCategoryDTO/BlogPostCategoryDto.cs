using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.BlogPostCategoryDto;

public class BlogPostCategoryDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string UrlHandle { get; set; }
}