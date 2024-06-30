using ERestaurant.Domain.Domain;
using ERestaurant.Service.Implementation;
using ERestaurant.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Domain.Domain;
using System.Security.Claims;

namespace ERestaurant.Web.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IMenuItemService _menuItemService;
        private readonly IShoppingCartService _shoppingCartService;

        public MenuItemsController(IMenuService menuService, IMenuItemService menuItemService, IShoppingCartService shoppingCartService)
        {
            _menuService = menuService;
            _menuItemService = menuItemService;
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult CreateMenuItem()
        {
            ViewBag.MenuList = new SelectList(_menuService.GetAllMenues(), "Id", "Name");
            return View(new MenuItem());
        }

        [HttpPost]
        public IActionResult CreateMenuItem(MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _menuItemService.CreateNewMenuItem(menuItem);
                return RedirectToAction("Index", "Restaurants"); // или друга соодветна акција
            }

            ViewBag.MenuList = new SelectList(_menuService.GetAllMenues(), "Id", "Name", menuItem.MenuId);
            return View(menuItem);
        }


        public IActionResult ListAllItemsInTheMenu(Guid menuId)
        {
            var items=_menuItemService.GetAllItemsInMenu(menuId);
            return View(items);
        }

        public IActionResult AddToCart(Guid? itemId)
        {
            if (itemId == null)
            {
                return NotFound();
            }

            var item = _menuItemService.GetDetailsMenuItem(itemId);

            ItemInShoppingCart ps = new ItemInShoppingCart();

            if (item != null)
            {
                ps.MenuItemId = item.Id;
            }

            return View(ps);
        }

        [HttpPost]
        public IActionResult AddToCartConfirmed(ItemInShoppingCart model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _shoppingCartService.AddToShoppingConfirmed(model, userId);



            return RedirectToAction("Index", "ShoppingCarts");


        }
    }
}
