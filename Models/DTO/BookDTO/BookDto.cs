using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.BookDTO;

public class BookDto
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
    public string Publisher { get; set; }
    [Required]
    public string PublishedDate { get; set; }
    public string ImageUrl { get; set; }
    public string WebReaderLink { get; set; }
    [Required]
    public string UrlHadle { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public List<BookCategoryDto.BookCategoryDto>? Categories { get; set; } = new ();
    [Required]
    public List<AuthorDto.AuthorDto>? Authors { get; set; } = new List<AuthorDto.AuthorDto>();
}
