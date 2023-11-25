using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO;

public class CreateFeedbackDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required] public string Message { get; set; }
}