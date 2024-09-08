using AdminApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace AdminApplication.Controllers
{
    public class DeliveryPersonController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://dinemaster.azurewebsites.net/api/DeliveryPerson/GetDeliveryPeople";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<DeliveryPerson>>().Result;

            return View(data);
        }

        public IActionResult Create()
        {
            HttpClient client = new HttpClient();
            string URL = "https://dinemaster.azurewebsites.net/api/RestaurantApi/GetAllRestaurants";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Restaurant>>().Result;
            ViewBag.RestaurantList = new SelectList(data, "Id", "Name");
            return View();
        }

        public IActionResult CreateDeliveryPerson([Bind("RestaurantId,Name,Phone,Vehicle")] DeliveryPerson deliveryPerson)
        {
            HttpClient client = new HttpClient();
            string URL = "https://dinemaster.azurewebsites.net/api/DeliveryPerson/CreateDeliveryPerson";
            var model = new DeliveryPerson
            {
                Id = Guid.NewGuid(),
                RestaurantId= deliveryPerson.RestaurantId,
                Name = deliveryPerson.Name,
                Phone = deliveryPerson.Phone,
                Vehicle= deliveryPerson.Vehicle
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            // var data = response.Content.ReadAsAsync<bool>().Result;
            return RedirectToAction("Index", "DeliveryPerson");
        }
    }
}
