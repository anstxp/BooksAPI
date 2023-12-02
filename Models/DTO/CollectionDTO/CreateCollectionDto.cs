using BooksAPI.Models.DTO.BookDTO;

namespace BooksAPI.Models.DTO.CollectionDTO;

public class CreateCollectionDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public string UrlHandle { get; set; }
}