using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.DeliveryDTO;

public class CreateDeliveryDto
{
    [Required]
    public string City { get; set; }
    [Required]
    public string Address { get; set; }
}