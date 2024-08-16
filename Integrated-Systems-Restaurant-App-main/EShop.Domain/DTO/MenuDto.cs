using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DTO
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
       // public Restaurant.Domain.Domain.Restaurant Restaurant { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<MenuItem>? MenuItems { get; set; }
    }
}
