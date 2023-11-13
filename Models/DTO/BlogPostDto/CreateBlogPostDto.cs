namespace BooksAPI.Models.DTO;

public class CreateBlogPostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public DateTime PublishDate { get; set; }
    public Guid UserId { get; set; }
    public bool IsVisible { get; set; }
    public Guid[] Categories { get; set; }
    public Guid[] Books { get; set; }
}