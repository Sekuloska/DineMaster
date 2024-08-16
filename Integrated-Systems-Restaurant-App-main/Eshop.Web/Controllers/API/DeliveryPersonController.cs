using ERestaurant.Domain.DTO;
using ERestaurant.Service.Implementation;
using ERestaurant.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Domain;

namespace ERestaurant.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        private readonly IDeliveryPersonService _eliveryPersonService;
        private readonly IRestaurantService _restaurantService;


        public DeliveryPersonController(IDeliveryPersonService eliveryPersonService, IRestaurantService restaurantService)
        {
            _eliveryPersonService = eliveryPersonService;
            _restaurantService = restaurantService;
        }
        [HttpGet("[action]")]
        public List<DeliveryPerson> GetDeliveryPeople()
        {
            return _eliveryPersonService.GetAllDeliveryPersons();
        }

        [HttpPost("[action]")]
        public bool CreateDeliveryPerson(DeliveryPersonDto deliveryPerson)
        {
            bool status = false;
            var rest = _restaurantService.GetDetailsForRestaurant(deliveryPerson.RestaurantId);

            var newDel = new DeliveryPerson
            {
                Id = deliveryPerson.Id,
                RestaurantId =deliveryPerson.RestaurantId,
                Restaurant = rest,
                Name = deliveryPerson.Name,
                Phone = deliveryPerson.Phone,
                Vehicle = deliveryPerson.Vehicle,

            };
            _eliveryPersonService.CreateDeliveryPersonAsync(newDel);
            status = true;
            return status;
        }
    }
}
