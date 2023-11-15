using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.BookDTO;

public class CreateBookDto
{
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
    public Guid[] Categories { get; set; }
    [Required]
    public Guid[] Authors { get; set; }
}