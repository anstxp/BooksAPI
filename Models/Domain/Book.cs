using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class Book
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public int PageCount { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    [Required]
    public string UrlHandle { get; set; }
    [Required]
    public int Price { get; set; }
    [ForeignKey("BookId")]
    public ICollection<BookCategory> Categories { get; set; }
    public ICollection<Author> Authors { get; set; }
    public ICollection<BlogPost> BlogPosts { get; set; }
    public ICollection<Comment> Comments { get; set; }
}