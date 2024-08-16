using AdminApplication.Models.ENUM;

namespace AdminApplication.Models.DTO
{
    public class OrderDto : BaseEntity
    {
        public string? userId { get; set; }
        public CostumerUser? User { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public string? DeliveryAddress { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<ItemInOrder>? ItemInOrders { get; set; }
        public virtual ICollection<Delivery>? Deliveries { get; set; }
    }
}
