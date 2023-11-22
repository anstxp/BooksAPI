namespace BooksAPI.Models.DTO.BlogPostDto;

public class UpdateBlogPostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTime PublishDate { get; set; }
    public bool IsVisible { get; set; }
    public List<Guid>? Books { get; set; } = new();
}