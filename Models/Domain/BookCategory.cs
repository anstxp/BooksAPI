using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class BookCategory
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public string UrlHandle { get; set; }
    [ForeignKey("CategoryId")]
    public ICollection<Book> Books { get; set; }
}