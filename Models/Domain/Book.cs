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
    public string UrlHadle { get; set; }
    [Required]
    public int Price { get; set; }
    [ForeignKey("CategoryId")]
    public ICollection<BookCategory> Categories { get; set; }
    [ForeignKey("AuthorId")]
    public ICollection<Author> Authors { get; set; }
    [ForeignKey("BlogPostId")]
    public ICollection<BlogPost> BlogPosts { get; set; }
}