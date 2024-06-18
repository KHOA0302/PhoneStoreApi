using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Ordering.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int PaymentId { get; set; }
        [Column(TypeName = "decimal(12,3)")]
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public bool IsUserDeleted { get; set; } = false;
        public string OrderStatus { get; set; } = "0";

        public Payment Payment { get; set; } = null!;
        public User User { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
