﻿using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DTO
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OpeningHours { get; set; }
        public int? Rating { get; set; }
        public string Description { get; set; }
        public string RestaurantImage { get; set; }
        //public virtual ICollection<Order>? Orders { get; set; }
       // public virtual ICollection<Menu>? Menues { get; set; }
    }
}
