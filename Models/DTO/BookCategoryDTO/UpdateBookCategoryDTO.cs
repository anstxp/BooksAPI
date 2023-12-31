namespace BooksAPI.Models.DTO.BookCategoryDto;

public class UpdateBookCategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string UrlHandle { get; set; }
    public List<Guid> Books { get; set; } = new List<Guid>();
}