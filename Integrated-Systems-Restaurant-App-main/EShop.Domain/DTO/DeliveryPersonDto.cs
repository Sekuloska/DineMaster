using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DTO
{
    public class DeliveryPersonDto
    {
        public Guid Id { get; set; }


        public Guid RestaurantId { get; set; }

       // public Restaurant Restaurant { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }


        public string Vehicle { get; set; }

       // public virtual ICollection<Delivery> Deliveries { get; set; }

    }
}
