using ERestaurant.Domain.Domain;
using ERestaurant.Domain.DTO;
using ERestaurant.Service.Interface;
using EShop.Domain.Identity;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X9;
using Restaurant.Domain.Domain;
using Restaurant.Domain.Enum;
using SQLitePCL;
using Stripe;
using System.Security.Claims;


namespace ERestaurant.Web.Controllers
{
    [Authorize]
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly UserManager<CostumerUser> _userManager;
        private readonly IRestaurantService _restaurantService;
        private readonly IOrderService _orderService;
        private readonly IMenuItemService _menuItemService;

        public ShoppingCartsController(IShoppingCartService _shoppingCartService, UserManager<CostumerUser> userManager, IRestaurantService restaurantService, IOrderService orderService,IMenuItemService menuItemService )
        {
            this._shoppingCartService = _shoppingCartService;
            this._userManager = userManager;
            this._restaurantService = restaurantService;
            this._orderService = orderService;
            this._menuItemService = menuItemService;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var dto = _shoppingCartService.getShoppingCartInfo(userId);

            return View(dto);
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = _shoppingCartService.order(userId);
            //if (res == 0) then throw;
            return RedirectToAction("Index", "ShoppingCarts");

        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

        public async Task<IActionResult> PayOrder(string stripeEmail, string stripeToken, OrderViewModel orderViewModel)
        {
            StripeConfiguration.ApiKey = "sk_test_51Io84IHBiOcGzrvu4sxX66rTHq8r5nxIxRiJPbOHB4NwVJOE1jSlxgYe741ITs024uXhtpBFtxm3RoCZc3kafocC00IhvgxkL0";
            var customerService = new CustomerService();
            var paymentIntentService = new PaymentIntentService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var rest = _restaurantService.GetDetailsForRestaurant(orderViewModel.SelectedRestaurantId);

            // Fetch the shopping cart information
            var order = _shoppingCartService.getShoppingCartInfo(userId);

            // Create or retrieve a customer
            var customerOptions = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken, // Attach the payment method (token) to the customer
            };
            var customer = customerService.Create(customerOptions);

            // Create a PaymentIntent
            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Currency = "usd",
                Customer = customer.Id,
                Description = "EShop Application Payment",
                OffSession = true, // Set to true for off-session payments
                Confirm = true, // Confirm the payment immediately
            };

            try
            {
                var paymentIntent = paymentIntentService.Create(paymentIntentOptions);

                if (paymentIntent.Status == "succeeded")
                {
                    // Call your order creation method or any other processing logic
                    var newOrder = new Order
                    {
                        userId = userId,
                        User=user,
                        RestaurantId = orderViewModel.SelectedRestaurantId,
                        Restaurant=rest,
                        OrderDate = DateTime.Now,
                        TotalAmount = (int)order.TotalPrice,
                        Status = OrderStatus.Proccessing,
                        DeliveryAddress=orderViewModel.Order.DeliveryAddress,
                        ItemInOrders = ConvertShoppingCartItemsToOrderItems(order.Products)


                    };
                    _orderService.CreateOrder(newOrder);

                    _shoppingCartService.ClearCart(userId);

                    return RedirectToAction("SuccessPayment");
                }
                else
                {
                    return RedirectToAction("NotsuccessPayment");
                }
            }
            catch (Exception ex)
            {
                // Log exception and handle errors
                Console.WriteLine($"Payment failed: {ex.Message}");
                return RedirectToAction("NotsuccessPayment");
            }
        }

        private ICollection<ItemInOrder> ConvertShoppingCartItemsToOrderItems(ICollection<ItemInShoppingCart> shoppingCartItems)
        {
            return shoppingCartItems.Select(item => new ItemInOrder
            {
                MenuItemId = item.MenuItemId,
                MenuItem = item.MenuItem, // Ensure this is correctly set if needed
                Quantity = item.Quantity
            }).ToList();
        }

        public IActionResult DeleteFromShoppingCart(Guid id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result=_shoppingCartService.deleteProductFromShoppingCart(user, id);
            
            return RedirectToAction("Index", "ShoppingCarts");
        }

    }
}
