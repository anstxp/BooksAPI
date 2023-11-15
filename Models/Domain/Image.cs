using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class Image
{
    public Guid Id { get; set; }
    [NotMapped]
    public IFormFile File { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public string FileExtension { get; set; }
    public long FileSizeInBytes { get; set; }
    public string FilePath { get; set; }
}