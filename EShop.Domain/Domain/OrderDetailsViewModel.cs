using System;
using System.Collections.Generic;
using Restaurant.Domain.Enum;

namespace Restaurant.Web.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public string DeliveryPersonName { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<ItemInOrderViewModel> Items { get; set; } = new List<ItemInOrderViewModel>();

        public class ItemInOrderViewModel
        {
            public string ItemName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}
