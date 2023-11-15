using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class BlogPost
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    [Required]
    public string UrlHandle { get; set; }
    [Required]
    public DateTime PublishDate { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public bool IsVisible { get; set; }
    [ForeignKey("CategoryId")]
    public ICollection<BlogPostCategory> Categories { get; set; }
    [ForeignKey("BookId")]
    public ICollection<Book> Books { get; set; }
}