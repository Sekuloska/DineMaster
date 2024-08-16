using ERestaurant.Domain.Domain;
using ERestaurant.Domain.DTO;
using ERestaurant.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Domain;

namespace ERestaurant.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuApiController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IRestaurantService _restaurantService;
        private readonly IMenuItemService _menuItemService;

        public MenuApiController(IMenuService menuService, IRestaurantService restaurantService, IMenuItemService menuItemService)
        {
            _menuService = menuService;
            _restaurantService = restaurantService;
            _menuItemService = menuItemService;
        }
       
        [HttpPost("[action]")]
        public bool CreateMenu(MenuDto menu)
        {
            bool status = false;
            var rest = _restaurantService.GetDetailsForRestaurant(menu.RestaurantId);

            var newMenu = new Menu
            {
                Id = menu.Id,
                RestaurantId = menu.RestaurantId,
                Restaurant = rest,
                Name = menu.Name,
                Description = menu.Description,
                IsActive = menu.IsActive

            };
            _menuService.CreateNewMenu(newMenu);
            status = true;
            return status;
        }


        [HttpGet("[action]")]
        public List<Menu> GetAllMenues()
        {
            return _menuService.GetAllMenues();
        }

        [HttpPost("[action]")]
        public bool CreateMenuItem(MenuItemDto menuItem)
        {
            bool status = false;
            var menu = _menuService.GetDetailsMenu(menuItem.MenuId);

            var newMenuItem = new MenuItem
            {
                Id=menuItem.Id,
                MenuId = menu.Id,
                Menu=menu,
                Name=menuItem.Name,
                Description=menuItem.Description,
                Price=menuItem.Price,
                IsAvailable=menuItem.IsAvailable,
                ItemImage=menuItem.ItemImage


            };
            _menuItemService.CreateNewMenuItem(newMenuItem);
            status = true;
            return status;
        }

        [HttpPost("[action]")]
        public List<Menu> GetMenuesForRestaurant(BaseEntity model)
        {
            return _menuService.GetMenusForRestaurant(model.Id);
        }

        [HttpPost("[action]")]
        public List<MenuItem> GetItemsForMenu(BaseEntity model)
        {
            return _menuItemService.GetAllItemsInMenu(model.Id);
        }




    }
}
