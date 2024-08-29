using ERestaurant.Domain.Domain;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Domain;
using Restaurant.Domain.Enum;

namespace ERestaurant.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("[action]")]
        public List<Order> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }


        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return _orderService.GetDetailsForOrder(model.Id);
        }


        [HttpPost("[action]")]
        public Guid AddOrder(Order model)
        {
            //var status = true;
            _orderService.UpdateOrder(model);

            return model.Id;
        }

        [HttpPatch("[action]/{Id}/{status}")]
        public Guid UpdateStatus(Guid Id, OrderStatus status)
        {
            //var status = true;
            _orderService.UpdateStatus(Id, status);

            return Id;
        }


    }
}
