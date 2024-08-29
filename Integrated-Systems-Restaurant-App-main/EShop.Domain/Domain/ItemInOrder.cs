using ERestaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Domain
{
    public class ItemInOrder:BaseEntity
    {
        public Guid? MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? Quantity { get; set; }

    }
}
