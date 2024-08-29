using ERestaurant.Repository.Interface;
using ERestaurant.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Domain.Domain;

namespace ERestaurant.Web.Controllers
{
    public class MenuesController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IRestaurantService _restaurantService;

        public MenuesController(IMenuService menuService, IRestaurantService restaurantService)
        {
            _menuService = menuService;
            _restaurantService = restaurantService;
        }


        public IActionResult Create()
        {
            ViewBag.RestaurantList = new SelectList(_restaurantService.GetAllRestaurants(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _menuService.CreateNewMenu(menu);
                return RedirectToAction("Index","Restaurants");
            }

            ViewBag.RestaurantList = new SelectList(_restaurantService.GetAllRestaurants(), "Id", "Name", menu.RestaurantId);
            return View(menu);
        }

        public IActionResult ListMenusForRestaurant(Guid restaurantId)
        {
            var menus = _menuService.GetMenusForRestaurant(restaurantId);
            return View(menus);
        }



    }
}
