using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.BlogPostCategoryDto;

public class CreateBlogPostCategoryDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string UrlHandle { get; set; }
}