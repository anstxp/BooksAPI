namespace BooksAPI.Models.Domain;

public class BlogPost
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
    public ICollection<BlogPostCategory> Categories { get; set; }
    public ICollection<Book> Books { get; set; }
}