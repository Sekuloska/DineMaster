﻿using ERestaurant.Domain.Domain;
using ERestaurant.Domain.Domain;
using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetDetailsForOrder(Guid? id);
        IEnumerable<Order> GetOrdersByUserId(string userId);
        void CreateOrder(Order order);
        void UpdateOrder(Order order); 

    }
}