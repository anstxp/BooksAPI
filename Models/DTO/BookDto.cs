using BooksAPI.Models.Domain;

namespace BooksAPI.Models.DTO;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int PageCount { get; set; }
    public string Publisher { get; set; }
    public string PublishedDate { get; set; }
    public string ImageUrl { get; set; }
    public string WebReaderLink { get; set; }
    public string UrlHadle { get; set; }
    public int Price { get; set; }
    public List<BookCategoryDto>? Categories { get; set; } = new List<BookCategoryDto>();
    public List<AuthorDto>? Authors { get; set; } = new List<AuthorDto>();
}
