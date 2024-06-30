using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Domain;
using Restaurant.Domain.Enum;
using ERestaurant.Domain.Domain;

namespace Restaurant.Domain.Domain
{
    public class Delivery:BaseEntity
    {
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid? DeliveryPersonId { get; set; }
        public DeliveryPerson? DeliveryPerson { get; set; }
        public DeliveryStatus? DeliveryStatus { get; set;}
        public DateFormat? DeliveryDate {  get; set; }


    }
}
