using ERestaurant.Domain.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Identity
{
    public class CostumerUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateFormat? DateRegistration { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }



    }
}
