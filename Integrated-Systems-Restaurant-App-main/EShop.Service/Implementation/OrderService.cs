using ERestaurant.Domain.Domain;
using ERestaurant.Domain.Domain;
using ERestaurant.Repository.Interface;
using EShop.Service.Interface;
using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERestaurant.Repository.Implementation;
using Restaurant.Domain.Enum;

namespace EShop.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll().ToList();
        }

        public Order GetDetailsForOrder(Guid? id)
        {
            return _orderRepository.Get(id);
        }

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            return _orderRepository.GetAll().Where(o => o.userId == userId)
                                     .ToList();
            //_context.Orders.Where(o => o.userId == userId).ToList();
        }
        public void CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            _orderRepository.Insert(order);  // Insert the new order into the repository
        }

        public void UpdateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            // Fetch the existing order to update it
            var existingOrder = _orderRepository.Get(order.Id);
            if (existingOrder == null)
            {
                throw new InvalidOperationException("Order not found");
            }

            // Update existing order properties with new values
            existingOrder.Status = order.Status;
            existingOrder.DeliveryAddress = order.DeliveryAddress;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.ItemInOrders = order.ItemInOrders; // Update items if necessary

            _orderRepository.Update(existingOrder);
        }

        public void UpdateStatus(Guid Id,OrderStatus status)
        {
            var order = _orderRepository.Get(Id);
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            order.Status = status;

            _orderRepository.Update(order);
        }
    }
}