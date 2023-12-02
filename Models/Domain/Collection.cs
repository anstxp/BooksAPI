using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class Collection
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UrlHandle { get; set; }
    public string Description { get; set; }
    [ForeignKey("CollectionId")]
    public ICollection<Book> Books { get; set; }
}