namespace BooksAPI.Models.Domain;

public class Author
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public ICollection<Book> Books { get; set; }
}