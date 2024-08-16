using ERestaurant.Domain.Domain;
using EShop.Domain.Identity;
using Restaurant.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain.Domain
{
    public class Order : BaseEntity
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
