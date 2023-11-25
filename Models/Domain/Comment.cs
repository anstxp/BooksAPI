using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public User User { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }

}