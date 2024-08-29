using ERestaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Domain
{
    public class DeliveryPerson:BaseEntity
    {
        [Required]
        public Guid RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Vehicle { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
