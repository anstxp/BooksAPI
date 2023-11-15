using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.BlogPostDto;

public class CreateBlogPostDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    [Required]
    public string UrlHandle { get; set; }
    [Required]
    public DateTime PublishDate { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public bool IsVisible { get; set; }
    public Guid[] Categories { get; set; }
    public Guid[] Books { get; set; }
}