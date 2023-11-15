using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class Author
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string? AuthorImageUrl { get; set; }
    [Required]
    public string UrlHandle { get; set; }
    [ForeignKey("BookId")]
    public ICollection<Book> Books { get; set; }
}