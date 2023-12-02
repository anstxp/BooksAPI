using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAPI.Models.Domain;

public class Order
{ 
        public Guid Id { get; set; } 
        public DateTime OrderDate { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal Total { get; set; }
}