namespace BooksAPI.Models.DTO;

public class UpdateBookDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int PageCount { get; set; }
    public string ImageUrl { get; set; }
    public string UrlHadle { get; set; }
    public int Price { get; set; }
    public List<Guid> Collections { get; set; } = new List<Guid>();
    public List<Guid> Categories { get; set; } = new List<Guid>();
    public List<Guid> Authors { get; set; } = new List<Guid>();
}