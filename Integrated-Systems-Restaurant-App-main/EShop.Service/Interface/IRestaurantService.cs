using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Interface
{
    public interface IRestaurantService
    {
        List<Restaurant.Domain.Domain.Restaurant> GetAllRestaurants();
        Restaurant.Domain.Domain.Restaurant GetDetailsForRestaurant(Guid? id);
        void CreateNewRestaurant(Restaurant.Domain.Domain.Restaurant rest);
        void UpdateNewRestaurant(Restaurant.Domain.Domain.Restaurant rest);
        void DeleteRestaurant(Guid? id);


    }
}
