using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class BlogPostCategory
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string UrlHandle { get; set; }
    [ForeignKey("BlogPostId")]
    public ICollection<BlogPost> BlogPosts { get; set; }
}