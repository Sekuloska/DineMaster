using ERestaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Domain
{
    public class Menu:BaseEntity
    {
        public Guid? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<MenuItem>? MenuItems { get; set; }

    }
}
