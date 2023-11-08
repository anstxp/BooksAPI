namespace BooksAPI.Models.DTO;

public class CreateBlogPostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string FeaturedImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public DateTime Date { get; set; }
    public string User { get; set; }
    public bool IsVisible { get; set; }
}