using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.ImageDto;

public class UploadImageDto
{
    [Required]
    public IFormFile File { get; set; }
}