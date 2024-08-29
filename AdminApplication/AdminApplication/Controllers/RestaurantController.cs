using AdminApplication.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdminApplication.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5196/api/RestaurantApi/GetAllRestaurants";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Restaurant>>().Result;
            return View(data);
        }

        public IActionResult ImportRestaurantsView()
        {
            return View();
        }
        public IActionResult ImportRestaurants(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream); ;
                fileStream.Flush();
            }

            List<Restaurant> restaurants = getAllRestaurantsFromFile(file.FileName);

            HttpClient client = new HttpClient();
            string URL = "http://localhost:5196/api/RestaurantApi/ImportAllRestaurants";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(restaurants), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<bool>().Result;
            return RedirectToAction("Index", "Order");

        }

        private List<Restaurant> getAllRestaurantsFromFile(string fileName)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {



                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        restaurants.Add(new Restaurant
                        {
                            Id = Guid.NewGuid(),
                            Name = reader.GetValue(0).ToString(),
                            Address = reader.GetValue(1).ToString(),
                            Phone = reader.GetValue(2).ToString(),
                            Email = reader.GetValue(3).ToString(),
                            OpeningHours = reader.GetValue(4).ToString(),
                            Rating = Convert.ToInt32(reader.GetValue(5)),
                            Description = reader.GetValue(6).ToString(),
                            RestaurantImage = reader.GetValue(7).ToString()

                        });



                    }

                }
            }
            return restaurants;
        }


                public IActionResult Create()
                {
                    return View();
                }

                public IActionResult CreateRestaurant([Bind("Name,Address,Phone,Email,OpeningHours,Rating,Description,RestaurantImage")] Restaurant restaurant)
                {
                    HttpClient client = new HttpClient();
                    string URL = "http://localhost:5196/api/RestaurantApi/AddRestaurant";
                    var model = new Restaurant
                    {
                        Id = Guid.NewGuid(),
                        Name = restaurant.Name,
                        Address = restaurant.Address,
                        Phone = restaurant.Phone,
                        Email = restaurant.Email,
                        OpeningHours = restaurant.OpeningHours,
                        Rating = restaurant.Rating,
                        Description = restaurant.Description,
                        RestaurantImage = restaurant.RestaurantImage

                    };
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(URL, content).Result;

                    var data = response.Content.ReadAsAsync<bool>().Result;
                    return RedirectToAction("Index", "Restaurant");
                }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound("Restaurant ID not provided.");
            }

            HttpClient client = new HttpClient();
            string URL = $"http://localhost:5196/api/RestaurantApi/GetRestaurantById/{id}";
            HttpResponseMessage response;

            try
            {
                response = client.GetAsync(URL).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string errorMsg = $"Failed to fetch restaurant details. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}";
                    return StatusCode((int)response.StatusCode, errorMsg);
                }

                var restaurant = response.Content.ReadAsAsync<Restaurant>().Result;

                if (restaurant == null)
                {
                    return NotFound("Restaurant not found.");
                }

                return View(restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,Address,Phone,Email,OpeningHours,Rating,Description,RestaurantImage")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest("Mismatched Restaurant ID.");
            }

            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }

            HttpClient client = new HttpClient();
            string URL = $"http://localhost:5196/api/RestaurantApi/UpdateRestaurant/{id}";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(URL, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update the restaurant. Please try again.");
                return View(restaurant);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound("Restaurant ID not provided.");
            }

            HttpClient client = new HttpClient();
            string URL = $"http://localhost:5196/api/RestaurantApi/GetRestaurantById/{id}";
            HttpResponseMessage response;

            try
            {
                response = client.GetAsync(URL).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string errorMsg = $"Failed to fetch restaurant details. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}";
                    return StatusCode((int)response.StatusCode, errorMsg);
                }

                var restaurant = response.Content.ReadAsAsync<Restaurant>().Result;

                if (restaurant == null)
                {
                    return NotFound("Restaurant not found.");
                }

                return View(restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(Guid id)
        {
            HttpClient client = new HttpClient();
            string URL = $"http://localhost:5196/api/RestaurantApi/DeleteRestaurant/{id}";

            HttpResponseMessage response = client.DeleteAsync(URL).Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500, "Failed to delete the restaurant. Please try again.");
            }

            return Ok();
        }





    }

}