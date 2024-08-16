using ERestaurant.Domain.Domain;
using ERestaurant.Domain.DTO;
using ERestaurant.Service.Interface;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ERestaurant.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantApiController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantApiController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

   
        [HttpGet("[action]")]
        public List<Restaurant.Domain.Domain.Restaurant> GetAllRestaurants()
        {
            return _restaurantService.GetAllRestaurants();
        }

        [HttpPost("[action]")]
        public bool AddRestaurant(ERestaurant.Domain.DTO.RestaurantDto model)
        {
            bool status = false;

            var newRest = new Restaurant.Domain.Domain.Restaurant
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone,
                Email = model.Email,
                OpeningHours = model.OpeningHours,
                Rating = model.Rating,
                Description = model.Description,
                RestaurantImage = model.RestaurantImage

            };
            _restaurantService.CreateNewRestaurant(newRest);
            status = true;
            return status;
        }

        [HttpPost("[action]")]
        public bool ImportAllRestaurants(List<RestaurantDto> model)
        {
            bool status = true;
            foreach (var rest in model)
            {
                
                    var neww = new Restaurant.Domain.Domain.Restaurant
                    {
                        Id=rest.Id,
                        Name = rest.Name,
                        Address = rest.Address,
                        Phone = rest.Phone,
                        Email = rest.Email,
                        OpeningHours = rest.OpeningHours,
                        Rating = rest.Rating,
                        Description = rest.Description,
                        RestaurantImage= rest.RestaurantImage
                    };
                _restaurantService.CreateNewRestaurant(neww);
            }
            return status;
        }

    }
}
