using AdminApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdminApplication.Controllers
{
    public class MenuItemController : Controller
    {
        public IActionResult ListAllItemsInTheMenu(Guid menuId)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5196/api/MenuApi/GetItemsForMenu";

            var model = new
            {
                Id = menuId
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<List<MenuItem>>().Result;

            return View(data);
        }
        [HttpPost]
        public IActionResult DeleteMenuItem(Guid menuItemId, Guid menuId)
        {
            HttpClient client = new HttpClient();
            string URL = $"http://localhost:5196/api/MenuApi/DeleteMenuItem/{menuItemId}";

            HttpResponseMessage response = client.DeleteAsync(URL).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Menu item deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting menu item.";
            }

            return RedirectToAction("ListAllItemsInTheMenu", new { menuId });
        }



    }
}
