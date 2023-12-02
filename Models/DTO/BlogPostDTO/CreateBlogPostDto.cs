using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.BlogPostDto;

public class CreateBlogPostDto
{
    public string Book { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTime PublishDate { get; set; }
    public Guid User { get; set; }
    public bool IsVisible { get; set; }
}