using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class Order
{
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public Delivery Delivery { get; set; }
    public decimal Total
    {
        get
        {
            decimal orderTotal = OrderItems.Sum(item => item.Quantity * item.Book.Price);
            decimal shippingCost = Delivery.DeliveryType.Price;

            return orderTotal + shippingCost;
        }
    }
}