using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.BookCategoryDto;

public class CreateBookCategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string UrlHandle { get; set; }
}