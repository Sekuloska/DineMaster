using ERestaurant.Domain.Domain;
using EShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Domain
{
    public class Review:BaseEntity
    {
        public string? userId { get; set; }
        public CostumerUser? User { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }


    }
}
