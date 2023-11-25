using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class Delivery
{
    public Guid Id { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Address { get; set; }
    public DeliveryType DeliveryType { get; set; }
}