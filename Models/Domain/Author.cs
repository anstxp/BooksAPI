using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class Author
{
    public Guid Id { get; set; }
    [Required]
    public string FullName { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string? Description { get; set; }
    [Required]
    public string UrlHandle { get; set; }
    public ICollection<Book> Books { get; set; }
}