using ERestaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Domain
{
    public class DeliveryPerson:BaseEntity
    {
        public Guid? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Vehicle { get; set; }
        public virtual ICollection<Delivery>? Deliveries { get; set; }
    }
}
