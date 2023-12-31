
using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.CollectionDTO;
using BooksAPI.Models.DTO.CommentDTO;

namespace BooksAPI.Models.DTO.BookDTO;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int PageCount { get; set; }
    public string ImageUrl { get; set; }
    public string UrlHadle { get; set; }
    public int Price { get; set; }
    public List<BookCategoryDto.BookCategoryDto>? Categories { get; set; } = new ();
    public List<CollectionDto>? Collection { get; set; } = new();
    public List<AuthorDto.AuthorDto>? Authors { get; set; } = new();
    public List<CommentDto>? Comments { get; set; } = new();
}
