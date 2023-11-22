using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BooksAPI.Models.DTO;
using BooksAPI.Models.DTO.AuthDTO;

namespace BooksAPI.Models.Domain;

public class BlogPost
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTime PublishDate { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public bool IsVisible { get; set; }
    [ForeignKey("UserId")]
    public ICollection<Book>? Books { get; set; }
    
}