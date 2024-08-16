using ERestaurant.Domain.DTO;
using ERestaurant.Service.Implementation;
using ERestaurant.Service.Interface;
using EShop.Domain.Identity;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Domain.Domain;
using Restaurant.Domain.Enum;
using System.Security.Claims;

namespace Restaurant.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<CostumerUser> _userManager;
        private readonly IRestaurantService _restaurantService;
        private readonly IShoppingCartService _shoppingCartService;

        public OrdersController(IOrderService orderService, UserManager<CostumerUser> userManager, IRestaurantService restaurantService, IShoppingCartService shoppingCartService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _restaurantService = restaurantService;
            _shoppingCartService = shoppingCartService;

        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = _orderService.GetOrdersByUserId(user.Id);
            return View(orders);
        }

        public async Task<IActionResult> MakeOrder()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            // Fetch the shopping cart information
            var shoppingCartDto = _shoppingCartService.getShoppingCartInfo(userId);

            // Fetch the list of restaurants
            var restaurants = _restaurantService.GetAllRestaurants();
            ViewBag.Restaurants = new SelectList(restaurants, "Id", "Name");
            var viewModel = new OrderViewModel
            {
                Order = new Order
                {
                    userId = userId,
                    User = user,
                    OrderDate = DateTime.Now,
                    TotalAmount = (int)shoppingCartDto.TotalPrice, // Set TotalAmount based on shopping cart
                    Status = OrderStatus.Proccessing,
                    DeliveryAddress = "", // This will be filled in the form
                    ItemInOrders = new List<ItemInOrder>() // This will be filled later
                },
              //  Restaurants = restaurants
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder(OrderViewModel viewModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var rest = _restaurantService.GetDetailsForRestaurant(viewModel.SelectedRestaurantId);

           
                
                var shoppingCartDto = _shoppingCartService.getShoppingCartInfo(userId);

                var newOrder = new Order
                {
                    userId = userId,
                    User = user,
                    RestaurantId = viewModel.SelectedRestaurantId,
                    Restaurant=rest,// Use the selected restaurant
                    OrderDate = DateTime.Now,
                    TotalAmount = (int)shoppingCartDto.TotalPrice, // Calculate TotalAmount
                    Status = OrderStatus.Proccessing,
                    DeliveryAddress = viewModel.Order.DeliveryAddress,
                    ItemInOrders = shoppingCartDto.Products.Select(p => new ItemInOrder
                    {
                        MenuItemId = p.MenuItem.Id,
                        Quantity = p.Quantity
                    }).ToList()
                };

                // Save the order to the database
                _orderService.CreateOrder(newOrder);

                return RedirectToAction("PayOrder", "ShoppingCarts"); // Redirect to a success page or handle as needed
            

        }
    }
}
