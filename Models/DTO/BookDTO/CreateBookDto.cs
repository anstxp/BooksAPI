using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.BookDTO;

public class CreateBookDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int PageCount { get; set; }
    public string ImageUrl { get; set; }
    public string UrlHadle { get; set; }
    public int Price { get; set; }
    public Guid[] Categories { get; set; }
    public Guid[] Collections { get; set; }
    public Guid[] Authors { get; set; }
}