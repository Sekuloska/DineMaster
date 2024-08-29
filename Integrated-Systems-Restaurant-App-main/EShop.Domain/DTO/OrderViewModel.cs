using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DTO
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
    //    public IEnumerable<Restaurant.Domain.Domain.Restaurant> Restaurants { get; set; } 
        public Guid? SelectedRestaurantId { get; set; } 
    }
}
