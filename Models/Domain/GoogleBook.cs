using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.Domain;

public  class GoogleBook
{
    [Key]
    public string Id { get; set; }
    public VolumeInfo volumeInfo { get; set; }
}

public  class VolumeInfo
{
    [Key] public string title { get; set; }
    public List<string> authors { get; set; }
}
