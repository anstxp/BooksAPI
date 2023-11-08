namespace BooksAPI.Models.Domain;

public class BlogPost
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public  Book Book { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string FeaturedImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public DateTime Date { get; set; }
    public string User { get; set; }
    public bool IsVisible { get; set; }
    
}