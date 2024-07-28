using ERestaurant.Service.Interface;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Domain.Enum;

namespace Restaurant.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IDeliveryPersonService _deliveryPersonService;

        public OrdersController(IOrderService orderService, IDeliveryPersonService deliveryPersonService)
        {
            _orderService = orderService;
            _deliveryPersonService = deliveryPersonService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var deliveryPersons = await _deliveryPersonService.GetAllDeliveryPersonsAsync();
            var deliveryStatuses = new List<SelectListItem>
            {
                new SelectListItem { Value = "Prepared", Text = "Prepared" },
                new SelectListItem { Value = "InProgress", Text = "In Progress" },
                new SelectListItem { Value = "Delivered", Text = "Delivered" }
            };

            ViewData["DeliveryPersons"] = new SelectList(deliveryPersons, "Id", "Name");
            ViewData["DeliveryStatuses"] = new SelectList(deliveryStatuses, "Value", "Text");

            return View(orders);
        }

        [HttpPost("api/changeDeliveryStatus")]
        public IActionResult ChangeDeliveryStatus(int orderId, string status)
        {
            if (!Enum.IsDefined(typeof(DeliveryStatus), status))
            {
                return BadRequest("Invalid status");
            }

            return Ok(new { Success = true });
        }

    }
}
