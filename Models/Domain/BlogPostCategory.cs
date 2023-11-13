namespace BooksAPI.Models.Domain;

public class BlogPostCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UrlHandle { get; set; }
    public ICollection<BlogPost> BlogPosts { get; set; }
}