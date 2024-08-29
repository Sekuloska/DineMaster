using ERestaurant.Domain.Domain;
using Microsoft.VisualBasic;
using Restaurant.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Domain
{
    public class Payment:BaseEntity
    {
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? Amount { get; set; }
        public DateFormat? DatePayment { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }

    }
}
