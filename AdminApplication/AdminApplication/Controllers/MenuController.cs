using AdminApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace AdminApplication.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Create()
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5196/api/RestaurantApi/GetAllRestaurants";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Restaurant>>().Result;
            ViewBag.RestaurantList = new SelectList(data, "Id", "Name");
            return View();
        }

        public IActionResult CreateMenu([Bind("Name,Description,IsActive,RestaurantId")] Menu menu)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5196/api/MenuApi/CreateMenu";
            var model = new Menu
            {
                Id = Guid.NewGuid(),
                RestaurantId=menu.RestaurantId,
                Name = menu.Name,
                Description = menu.Description,
                IsActive = menu.IsActive
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

           // var data = response.Content.ReadAsAsync<bool>().Result;
            return RedirectToAction("Index", "Restaurant");
        }
        public IActionResult AddItemToMenu()
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5196/api/MenuApi/GetAllMenues";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Menu>>().Result;
            ViewBag.MenuList = new SelectList(data, "Id", "Name");
            return View();
        }

        public IActionResult CreateMenuItem([Bind("MenuId,Name,Description,Price,IsAvailable,ItemImage")] MenuItem menuItem)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5196/api/MenuApi/CreateMenuItem";
            var model = new MenuItem
            {
                Id = Guid.NewGuid(),
                MenuId = menuItem.MenuId,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                IsAvailable = menuItem.IsAvailable,
                ItemImage=menuItem.ItemImage
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            // var data = response.Content.ReadAsAsync<bool>().Result;
            return RedirectToAction("Index", "Restaurant");
        }

        public IActionResult ListMenusForRestaurant(Guid restaurantId)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5196/api/MenuApi/GetMenuesForRestaurant";

            var model = new
            {
                Id = restaurantId
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<List<Menu>>().Result;
            
            return View(data);
        }
    }
}
