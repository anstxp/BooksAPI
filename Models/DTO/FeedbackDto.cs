namespace BooksAPI.Models.DTO;

public class FeedbackDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}