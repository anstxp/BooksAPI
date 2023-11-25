using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.Domain;

public class Feedback
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [Required]
    public string Phone { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}