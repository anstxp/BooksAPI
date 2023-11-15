using System.ComponentModel.DataAnnotations;
using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO.BlogPostDto;

public class BlogPostDto
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
    public List<BlogPostCategoryDto.BlogPostCategoryDto>? Categories { get; set; } = new();
    public List<BookDto>? Books { get; set; } = new();
    
}