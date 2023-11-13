namespace BooksAPI.Models.DTO;

public class BlogPostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public DateTime PublishDate { get; set; }
    public Guid UserId { get; set; }
    public bool IsVisible { get; set; }
    public List<BlogPostCategoryDto>? Categories { get; set; } = new();
    public List<BookDto>? Books { get; set; } = new();
    
}