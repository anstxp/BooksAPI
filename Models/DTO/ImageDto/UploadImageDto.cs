using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.ImageDto;

public class UploadImageDto
{
    [Required]
    public IFormFile File { get; set; }
    [Required]
    public string Name { get; set; }
    public string? FileDescription { get; set; }
    
    
}