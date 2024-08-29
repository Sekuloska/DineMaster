using ERestaurant.Repository.Interface;
using ERestaurant.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Implementation
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<Restaurant.Domain.Domain.Restaurant> _restaurantRepository;
        public RestaurantService(IRepository<Restaurant.Domain.Domain.Restaurant> restaurantRepository)
        {
            _restaurantRepository= restaurantRepository;
        }

       
        public void CreateNewRestaurant(Restaurant.Domain.Domain.Restaurant rest)
        {
            _restaurantRepository.Insert(rest);
        }

        public void DeleteRestaurant(Guid? id)
        {
            var restaurant = _restaurantRepository.Get(id);
            _restaurantRepository.Delete(restaurant);
        }

        public List<Restaurant.Domain.Domain.Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll().ToList();
        }

        public Restaurant.Domain.Domain.Restaurant GetDetailsForRestaurant(Guid? id)
        {
            return _restaurantRepository.Get(id);
        }

        public void UpdateNewRestaurant(Restaurant.Domain.Domain.Restaurant rest)
        {
            _restaurantRepository.Update(rest);
        }
    }
}
